using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
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
            var lala = entities.Content.ToList();
            return lala;
        }
    }
}