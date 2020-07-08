using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("comment")]
    [Route("[controller]")]
    [ApiController]
    public class CommentApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance(); 

        [HttpGet]
        public List<Comment> get()
        {
            var comments = entities.Comments.ToList();
            return comments;
        }
    }
}