using Data.Models;
using Logic.Exceptions;
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
        public List<Document> GetDocumentsNeeded(int personId, int courseId)
        {
            List<Document> limitedDocuments = new List<Document>();
            if (personId != 0)
            {
                List<Document> documents = entities.RelDocumentClasses.Where(x => x.ClassId == personId && x.Class == "Person").Select(c => c.Document).ToList();

                ///if courseId is not null, we want to limit the documents and only search those
                ///with a relation to one Person and one special course
                if (courseId != 0)
                {
                    foreach (var item in documents)
                    {
                        if (entities.RelDocumentClasses.FirstOrDefault(x => x.DocId == item.Id && x.Class == "Course" && x.ClassId == courseId) != null)
                        {
                            limitedDocuments.Add(item);
                        }
                    }
                    return ConvertDocuments(limitedDocuments);
                }
                else
                {
                    return ConvertDocuments(documents);
                }
            }
            //here we want to get only Documents with relation to one Course...that means for example a CoffeeList
            //documents with both...relation to Person and relation to Course are going to be removed
            else if (personId == 0 && courseId != 0)
            {
                List<Document> documents = entities.RelDocumentClasses.Where(x => x.ClassId == courseId && x.Class == "Course").Select(c => c.Document).ToList();

                foreach (var item in documents)
                {
                    if (entities.RelDocumentClasses.FirstOrDefault(x => x.DocId == item.Id && x.Class == "Person") != null)
                    {
                        limitedDocuments.Add(item);
                    }
                }
                foreach (var item in limitedDocuments)
                {
                    documents.Remove(item);
                }
                return ConvertDocuments(documents);
            }
            else throw new MissingInputException("You have to enter either Person or Course or both.");
        }

        private List<Document> ConvertDocuments(List<Document> documents)
        {
            if (documents.Count > 0)
            {
                foreach (Document document in documents)
                {
                    ConvertDocumentStringToBase64AndAttach(document);
                }
                return documents;
            }
            else throw new EntryCouldNotBeFoundException("Documents could not be found.");
        }

        /// <summary>
        /// Creates a new document
        /// </summary>
        /// <param name="recDocument"></param>
        /// <returns>Document</returns>
        public Document CreateNewDocument(Document recDocument)
        {
            CheckIfIdToConnectWithExists(recDocument);

            if (recDocument.DocumentString != null)
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
            Document documentToDelete = entities.Documents.SingleOrDefault(x => x.Id == id);
            if (documentToDelete == null)
            {
                throw new EntryCouldNotBeFoundException("The Document you want to delete could not be found.");
            }
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

            ///Deletes Document with its Path
            DeleteRealDocument(documentToDelete);
            ///Deletes Document entry in Database
            entities.Documents.Remove(documentToDelete);
            entities.SaveChanges();

            return "Record has been successfully deleted";
        }

        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="documentToDelete"></param>
        /// <returns></returns>
        public void DeleteRealDocument(Document documentToDelete)
        {
            string filename = documentToDelete.Url;

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            else
            {
                throw new FileDoesNotExistException("It seems that the File you want to delete does not exist.");
            }
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
            string name = Type.ToString() + "_" + person.LastName + "_" + DateTime.Now.ToFileTime() + fileExtension;
            return name;
        }

        /// <summary>
        /// prevents wrong entries, because of not existing Ids
        /// </summary>
        /// <param name="document"></param>
        /// <returns>document</returns>
        public void CheckIfIdToConnectWithExists(Document document)
        {
            Person person = null;
            Course course = null;

            if (document.PersonId == null && document.CourseId == null)
            {
                throw new MissingInputException("You have to enter either Person or Course or both.");
            }
            if (document.PersonId != null)
            {
                person = entities.Persons.FirstOrDefault(c => c.Id == document.PersonId);
                if (person == null)
                {
                    throw new EntryCouldNotBeFoundException("The Person you want to assign to this document could not be found.");
                }
            }
            if (document.CourseId != null)
            {
                course = entities.Courses.FirstOrDefault(c => c.Id == document.CourseId);
                if (course == null)
                {
                    throw new EntryCouldNotBeFoundException("The Course you want to assign to this document could not be found.");
                }
            }
        }
    }
}