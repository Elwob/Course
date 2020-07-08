
using Data.Entities;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
            entities.Documents.Remove(entities.Documents.Single(x => x.Id == id));        
            entities.SaveChanges();
            return "Record has successfully Deleted";
        }


    }
}