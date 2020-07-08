
using Data.Entities;
using Data.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Renci.SshNet.Messages;
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
            foreach(var item in relationList)
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
            DeleteRealDocument(documentToDelete);
            ///Deletes Document entry in Database
            entities.Documents.Remove(documentToDelete);        
            entities.SaveChanges();
            return "Record has successfully Deleted";
        }
        public void DeleteRealDocument(Document documentToDelete)
        {
            try
            {
                string filename = documentToDelete.Url;             

                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                else
                {
                    Debug.WriteLine("File does not exist.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void CreateDocumentFromTemplate(string url, EDocumentType Type, Person person, int courseId)
        {
            Document newDoc = new Document();
            newDoc.Url = url;
            ///should cut the path, that we can get the Name
            newDoc.Name = url.Substring(documentMainPath.Length + 1);
            newDoc.Comment = "Document created from Template";
            newDoc.Type = Type;
            newDoc.CourseId = courseId;
            newDoc.PersonId = person.Id;
            Document document = CreateNewDocument(newDoc);
            ///jetzt document der Communication hinzufügen
        }
    }
}