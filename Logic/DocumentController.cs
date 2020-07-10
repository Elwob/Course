using Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Document = Data.Models.Document;

namespace Logic
{
    public class DocumentController : MainController
    {
        private CommunicationController communicationController = new CommunicationController();

        public List<Document> GetDocumentsNeeded(int id, EClass className)
        {
            List<Document> documents = entities.RelDocumentClasses.Where(x => x.ClassId == id && x.Class == className.ToString()).Select(c => c.Document).ToList();
            return documents;
        }

        public Document CreateNewDocument(Document recDocument)
        {
            recDocument.CreatedAt = DateTime.Now;
            recDocument.ModifiedAt = DateTime.Now;

            entities.Documents.Attach(recDocument);
            recDocument.CreateRelation();
            entities.SaveChanges();
            return recDocument;
        }

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

            Document documentToDelete = entities.Documents.Single(x => x.Id == id);
            ///Deletes Document with its Path
            bool fileFound = DeleteRealDocument(documentToDelete);
            ///Deletes Document entry in Database
            entities.Documents.Remove(documentToDelete);
            entities.SaveChanges();
            if (fileFound)
            {
                return "Record has successfully Deleted";
            }
            else
            {
                return "File not found.";
            }
            
        }

        public bool DeleteRealDocument(Document documentToDelete)
        {
            bool fileFound = true;
            try
            {
                string filename = documentToDelete.Url;

                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                else
                {
                    fileFound = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return fileFound;
        }

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

        public string CreateFileName(EDocumentType Type, Person person, string fileExtension)
        {
            string name = Type.ToString() + "_" + person.LastName + "_" + DateTime.Now.ToFileTime() + fileExtension;
            return name;
        }
    }
}