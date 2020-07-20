using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    [Route("document")]
    [Route("[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private DocumentController documentController = new DocumentController();

        [HttpGet("{id}/{className}")]
        public List<Document> GetVariousDocuments(int id, EClass className)

        {
            var documents = documentController.GetDocumentsNeeded(id, className);

            return documents;
        }

        [Route("getDocumentTypes")]
        [HttpGet]
        public List<string> GetEnumsDocumentType()
        {
            var enums = documentController.GetEnums<EDocumentType>();
            return enums;
        }

        [HttpPost]
        public Document Post([FromBody] Document recDocument)
        {
            Document latestDocument = documentController.CreateNewDocument(recDocument);
            return latestDocument;
        }

        [HttpDelete("{id}")]
        public string DeleteById(int id)
        {
            return documentController.DeleteById(id);
        }
    }
}