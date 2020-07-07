
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
            recDocument.CreatedAt = DateTime.Now;
            recDocument.ModifiedAt = DateTime.Now;

            entities.Documents.Attach(recDocument);
            recDocument.CreateRelation();
            entities.SaveChanges();
            return recDocument;
        }

       
    }
}