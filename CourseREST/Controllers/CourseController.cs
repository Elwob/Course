using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CourseEntities entities = new CourseEntities();

        [HttpGet]
        public List<Course> get()
        {
            var courses = entities.Courses.Include(c => c.CourseContents).ThenInclude(x => x.Content).Include(x => x.CourseSubventions).ThenInclude(x => x.Subvention).ToList();
            return courses;
        }
    }
}
