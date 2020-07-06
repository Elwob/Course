using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class DocumentController : MainController
    {

        public static DocumentController instance = null;

        public static DocumentController GetInstance()
        {
            if (instance == null)
            {
                instance = new DocumentController();
            }

            return instance;
        }
        public List<Document> GetDocumentsNeeded(int id, EClass className)
        {
            List<Document> documents = entities.RelDocumentClasses.Where(x => x.ClassId == id && x.Class == className.ToString()).Select(c => c.Document).ToList(); 
            return documents;
        }
        public Document CreateNewDocument(Document recDocument)
        {
            Document document = new Document();
            document.Url = recDocument.Url;
            document.Name = recDocument.Name;
            document.Comment = recDocument.Comment;
            document.CreatedAt = DateTime.Now;
            document.Type = recDocument.Type;
            document.CourseId = recDocument.CourseId;
            document.PersonId = recDocument.PersonId;

            entities.Documents.Add(document);
            entities.SaveChanges();
            Document latestDocument = entities.Documents.LastOrDefault();

            if(recDocument.CourseId != null)
            {
                RelDocumentClass relDocumentClass = new RelDocumentClass();
                relDocumentClass.DocId = latestDocument.Id;
                relDocumentClass.Class = EClass.Course.ToString();
                relDocumentClass.ClassId = (int)recDocument.PersonId;
                entities.RelDocumentClasses.Add(relDocumentClass);
                entities.SaveChanges();
            }
            if (recDocument.PersonId != null)
            {
                RelDocumentClass relDocumentClass = new RelDocumentClass();
                relDocumentClass.DocId = latestDocument.Id;
                relDocumentClass.Class = EClass.Person.ToString();
                relDocumentClass.ClassId = (int)recDocument.PersonId;
                entities.SaveChanges();
            }

            return entities.Documents.LastOrDefault();
        }


    }
}