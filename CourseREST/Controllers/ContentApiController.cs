using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Content> get()

        {
            var content = entities.Contents.ToList();
            return content;
        }


    }
}