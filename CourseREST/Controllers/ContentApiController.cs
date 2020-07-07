using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using Logic;
using Data.Models;

namespace CourseREST.Controllers
{
    [Route("content")]
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {


        ContentController contentController = ContentController.GetInstance();

        [HttpGet]
        public List<Content> Get()
        {
            return contentController.GetAllContents();
        }

        [HttpPost]
        public Content Post([FromBody] Content recContent)
        {
            return contentController.PostContent(recContent);
            //return null;
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