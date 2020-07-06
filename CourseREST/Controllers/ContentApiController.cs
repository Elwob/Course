using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Data.Models.ReceiveModels;


namespace CourseREST.Controllers
{
    [Route("content")]
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {
        ContentController contentController = new ContentController();

        [HttpGet]
        public List<Content> Get()
        {
            return contentController.GetAllContents();
        }

        [HttpPost]
        public Content Post([FromBody] ReceivedContent recContent)
        {
            return contentController.PostContent(recContent);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Content content)
        {
            contentController.PutContent(id, content);
            
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contentController.DeleteContent(id);
        }
    }
}