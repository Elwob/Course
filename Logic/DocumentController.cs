using Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Document = Data.Models.Document;

namespace Logic
{
    public class DocumentController : MainController
    {
        private CommunicationController communicationController = new CommunicationController();
        /// <summary>
        /// Gets documents concerning one course or one person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="className"></param>
        /// <returns>List<Document></returns>
        public List<Document> GetDocumentsNeeded(int id, EClass className)
        {
            List<Document> documents = entities.RelDocumentClasses.Where(x => x.ClassId == id && x.Class == className.ToString()).Select(c => c.Document).ToList();
            if (documents.Count > 0)
            {
                foreach (Document document in documents)
                {
                    ConvertDocumentStringToBase64AndAttach(document);
                }
                return documents;
            }
            else return null;        
        }
        /// <summary>
        /// Creates a new document
        /// </summary>
        /// <param name="recDocument"></param>
        /// <returns>Document</returns>
        public Document CreateNewDocument(Document recDocument)
        {
            recDocument = CheckIfIdToConnectWithExists(recDocument);
            if (recDocument == null)
            {
                return null;
            }
            if(recDocument.DocumentString != null)
            {
                recDocument = ConvertDocumentStringFromBase64AndSave(recDocument, ".pdf");
            }
            recDocument.CreatedAt = DateTime.Now;
            recDocument.ModifiedAt = DateTime.Now;

            entities.Documents.Attach(recDocument);
            recDocument.CreateRelation();
            entities.SaveChanges();
            return recDocument;
        }
        /// <summary>
        /// Creates a unique file name, converts the DocumentString from Base 64 into Bytes and writes them into destination file
        /// </summary>
        /// <param name="document"></param>
        /// <param name="fileExtension"></param>
        /// <returns>Document</returns>
        public Document ConvertDocumentStringFromBase64AndSave(Document document, string fileExtension)
        {
            string fileName = document.Name + "_" + DateTime.Now.ToFileTime() + fileExtension;
            string destFile = documentMainPath + "\\DocumentsUploaded\\" + fileName;
            System.IO.File.WriteAllBytes(destFile, Convert.FromBase64String(document.DocumentString));
            document.Url = destFile;
            document.Name = fileName;
            return document;
        }
        /// <summary>
        /// Converts the DocumentString to Base64 and attaches it to the document we want to return
        /// </summary>
        /// <param name="document"></param>
        public void ConvertDocumentStringToBase64AndAttach(Document document)
        {
            var fileAsString = Convert.ToBase64String(System.IO.File.ReadAllBytes(document.Url));
            document.DocumentString = fileAsString;
            
        }
        /// <summary>
        /// Deletes a document by its Id, removes entry in RelDocumentClasses and sets DocumentId in Absences and Communications to null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteById(int id)
        {
            ///delete the Relations from Documents to Classes
            List<RelDocumentClass> relationList = entities.RelDocumentClasses.Where(x => x.DocId == id).ToList();
            foreach (var item in relationList)
            {
                entities.RelDocumentClasses.Remove(item);
            }
            ///set the affected Absences - DocumentId to null
            List<Absence> absenceList = entities.Absences.Where(x => x.DocumentId == id).ToList();
            foreach (var item in absenceList)
            {
                item.DocumentId = null;
                entities.Absences.Update(item);
            }
            ///set the affected Communications - DocumentId to null
            List<Communication> communicationList = entities.Communications.Where(x => x.DocumentId == id).ToList();
            foreach (var item in communicationList)
            {
                item.DocumentId = null;
                entities.Communications.Update(item);
            }

            Document documentToDelete = entities.Documents.SingleOrDefault(x => x.Id == id);
            if (documentToDelete == null)
            {
                return "The Document you want to delete could not be found.";
            }
            ///Deletes Document with its Path
            bool fileFound = DeleteRealDocument(documentToDelete);
            ///Deletes Document entry in Database
            entities.Documents.Remove(documentToDelete);
            entities.SaveChanges();

            if (fileFound)
            {
                return "Record has been successfully deleted";
            }
            else
            {
                return "File not found.";
            }
        }
        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="documentToDelete"></param>
        /// <returns></returns>
        public bool DeleteRealDocument(Document documentToDelete)
        {
            bool fileFound = true;

            string filename = documentToDelete.Url;

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            else
            {
                fileFound = false;
            }         

            return fileFound;
        }
        /// <summary>
        /// receives data from Email_TemplateController, creates a Document and then a communication on database
        /// </summary>
        /// <param name="template"></param>
        /// <param name="person"></param>
        /// <param name="reminderId"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns>Communication</returns>
        public Communication CreateDocumentFromTemplate(EmailTemplate template, Person person, int? reminderId, string url, string name)
        {
            Document newDoc = new Document();
            newDoc.Name = name;
            newDoc.Url = url;
            newDoc.Comment = "Document created from Template";
            newDoc.Type = template.DocumentType;
            newDoc.CourseId = template.CourseId;
            newDoc.PersonId = person.Id;
            Document document = CreateNewDocument(newDoc);
            ///in this case Date = DateTime.Now, but can be different if we would make an entry about last weeks phone call
            DateTime date = DateTime.Now;
            Communication communication = communicationController.CreateCommunication(document, template, date, reminderId);
            return communication;
        }
        /// <summary>
        /// creates unique fileName for Documents which are generated from Template
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="person"></param>
        /// <param name="fileExtension"></param>
        /// <returns>string</returns>
        public string CreateFileName(EDocumentType Type, Person person, string fileExtension)
        {
            string name = null;
            if (person != null)
            {
                name = Type.ToString() + "_" + person.LastName + "_" + DateTime.Now.ToFileTime() + fileExtension;
            }
            return name;
        }

        /// <summary>
        /// prevents wrong entries, because of not existing CourseId or PersonId
        /// </summary>
        /// <param name="document"></param>
        /// <returns>document</returns>
        public Document CheckIfIdToConnectWithExists(Document document)
        {
            var person = entities.Persons.FirstOrDefault(c => c.Id == document.PersonId);
            var course = entities.Courses.FirstOrDefault(c => c.Id == document.CourseId);
            if (person == null && course == null)
            {
                return null;
            }
            else if (person == null && course != null)
            {
                document.PersonId = null;
                return document;
            }
            else if (course == null && person != null)
            {
                document.CourseId = null;
                return document;
            }
            else return document;
        }
    }
}