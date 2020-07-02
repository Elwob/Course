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
        public List<Course> get()
        {
            var courses = entities.Courses.Include(c => c.CourseContents).ThenInclude(x => x.Content).Include(x => x.CourseSubventions).ThenInclude(x => x.Subvention).ToList();
            //var lala = entities.Content.ToList();
            return courses;
        }
    }
}