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

        [HttpGet]
        public List<Content> Get()
        {
            return contentController.GetAllContents();
        }

        [HttpPost]
        public Content Post([FromBody] Content recContent)
        {
            return contentController.PostContent(recContent);
        }

        [HttpPut("{id}")]
        public Content Put(int id, [FromBody] Content content)
        {
            return contentController.PutContent(id, content);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contentController.DeleteContent(id);
        }
    }
}