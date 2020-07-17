using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
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
            return contentController.GetAllContents();
        }

        /// <summary>
        /// creates a new content in DB
        /// </summary>
        /// <param name="recContent"></param>
        /// <returns></returns>
        [HttpPost]
        public Content Post([FromBody] Content recContent)
        {
            return contentController.PostContent(recContent);
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
            return contentController.PutContent(id, content);
        }

        /// <summary>
        /// deletes a certain content in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contentController.DeleteContent(id);
        }
    }
}