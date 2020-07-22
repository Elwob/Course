using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning contents
    /// </summary>
    [Route("content")]
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {
        private ContentController contentController = new ContentController();

        public ContentApiController()
        {
            Data.Models.Content.ShouldIgnoreRelation = false;
        }

        /// <summary>
        /// returns all contents existing in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Content> Get()
        {
            List<Content> contentList = null;
            try
            {
                contentList = contentController.GetAllContents();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return contentList;
        }

        /// <summary>
        /// creates a new content in DB
        /// </summary>
        /// <param name="recContent"></param>
        /// <returns></returns>
        [HttpPost]
        public Content Post([FromBody] Content recContent)
        {
            Content returnContent = null;
            try
            {
                returnContent = contentController.PostContent(recContent);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnContent;
        }

        /// <summary>
        /// updates a content in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Content Put(int id, [FromBody] Content content)
        {
            Content returnContent = null;
            try
            {
                returnContent = contentController.PutContent(id, content);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnContent;
        }

        /// <summary>
        /// deletes a certain content in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                contentController.DeleteContent(id);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
        }
    }
}