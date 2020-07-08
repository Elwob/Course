using Data.Entities;
using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourseREST.Controllers
{
    [Route("document")]
    [Route("[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private DocumentController documentController = new DocumentController();
        private CourseEntities entities = CourseEntities.GetInstance();


        [HttpGet("{id}/{className}")]
     
        public List<JObject> GetVariousDocuments(int id, EClass className)

        {
            var documents = documentController.GetDocumentsNeeded(id, className);
            
            ///that Enums will be shown correctly in JSON
            List<JObject> jsons = SerializeAndCreateJsonObject<Document>(documents);

            return jsons;
        }

        public static List<JObject> SerializeAndCreateJsonObject<T>(List<T> list)
        {
            var jasonString = "";
            List<JObject> jsons = new List<JObject>();

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.WriteIndented = true;

            foreach (var item in list)
            {
                jasonString = JsonSerializer.Serialize(item, options);
                ///Parse into JSON - Objects for better usability in Frontend
                JObject json = JObject.Parse(jasonString);
                jsons.Add(json);
            }

            return jsons;
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
        [HttpDelete ("{id}")]
        public string DeleteById(int id)
        {
            return documentController.DeleteById(id);  
        }
    }
}
