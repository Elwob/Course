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
    public class ContentController : ControllerBase
    {
        private CourseEntities entities = new CourseEntities();

        [HttpGet]
        public List<Content> get()

        {
            var content = entities.Contents.ToList();
            return content;
        }


    }
}