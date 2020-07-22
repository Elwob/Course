using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning documents
    /// </summary>
    [Route("document")]
    [Route("[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private DocumentController documentController = new DocumentController();
        /// <summary>
        /// Gets Documents by Course Id, or Person Id...later on maybe more EClasses can be used
        /// </summary>
        /// <param name="id"></param>
        /// <param name="className"></param>
        /// <returns>List<Document></Document></returns>
        [HttpGet("{id}/{className}")]
        public List<Document> GetVariousDocuments(int id, EClass className)
        {
            List<Document> documents = null;
            try
            {
                documents = documentController.GetDocumentsNeeded(id, className);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return documents;
        }
        /// <summary>
        /// Provides Enums EDocumentType
        /// </summary>
        /// <returns>List<string></string></returns>
        [Route("getDocumentTypes")]
        [HttpGet]
        public List<string> GetEnumsDocumentType()
        {
            List<string> enums = null;
            try
            {
                enums = documentController.GetEnums<EDocumentType>();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return enums;
        }
        /// <summary>
        /// We get a Document POST which includes the Document-String encoded in Base64
        /// </summary>
        /// <param name="recDocument"></param>
        /// <returns>Document document</returns>
        [HttpPost]
        public Document Post([FromBody] Document recDocument)
        {
            Document latestDocument = null;
            try
            {
                latestDocument = documentController.CreateNewDocument(recDocument);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return latestDocument;
        }
        /// <summary>
        /// Delete one Document by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        [HttpDelete("{id}")]
        public string DeleteById(int id)
        {
            string responseString = null;
            try
            {
                responseString = documentController.DeleteById(id);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return responseString;
        }
    }
}