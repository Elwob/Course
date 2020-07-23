using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning Contents
    /// </summary>
    [Route("content")]
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {
        private ContentController contentController = new ContentController();

        // not needed anymore, since courses are now returned as JSONCourseSend, which is generated from JSONConverter
        public ContentApiController()
        {
            Data.Models.Content.ShouldIgnoreRelation = false;
        }

        /// <summary>
        /// gets all Contents existing in DB
        /// </summary>
        /// <returns>a list of Contents</returns>
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
        /// creates a new Content in DB
        /// </summary>
        /// <param name="recContent"></param>
        /// <returns>the created Content</returns>
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
        /// updates a Content in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns>the updated Content</returns>
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
        /// deletes a certain Content in DB
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