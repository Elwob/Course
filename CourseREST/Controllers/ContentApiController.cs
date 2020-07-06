using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CourseREST.Controllers
{
    [Route ("content")]
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Content> Get()
        {
            var content = entities.Contents.ToList();
            // use following line if you want to return relations to courses (where a content is teached in) as well:
            // var content = entities.Contents.Include(c => c.CourseContents).ThenInclude(x => x.Course).ToList();
            return content;
        }

        [HttpPost]
        public Content Post([FromBody] ReceiveModels.ReceivedContent recContent)
        {
            entities.Contents.Add(new Content(recContent.Topic, recContent.Description, recContent.UnitEstimation));
            entities.SaveChanges();
            return entities.Contents.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}