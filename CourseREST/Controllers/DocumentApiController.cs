﻿using Data.Entities;
using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("document")]
    [Route("[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private DocumentController documentController = new DocumentController();
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Document> get()
        {
            var documents = entities.Documents.ToList();
            return documents;
        }
        [HttpGet("{id}/{className}")]

        public List<Document> GetVariousDocuments(int id, EClass className)
        {
            var documents = documentController.GetDocumentsNeeded(id, className);
            return documents;
        }
        [HttpPost]
        public Document Post([FromBody] Document recDocument)
        {
            Document latestDocument = documentController.CreateNewDocument(recDocument);
            //course id und person id sind null obwohl von postman geschickt....Master!!!
            return latestDocument;
        }
    }
}
