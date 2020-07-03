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
    [Route("course")]
    [Route("[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Course> get()
        {
            var courses = entities.Courses.Include(c => c.CourseContents).ThenInclude(x => x.Content).Include(x => x.CourseSubventions).ThenInclude(x => x.Subvention).ToList();
            return courses;
        }
    }
}
