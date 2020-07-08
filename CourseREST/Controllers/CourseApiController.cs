using Data.Models;
using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourseREST.Controllers
{
    [Route("course")]
    [Route("[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private CourseController courseController = new CourseController();

        [HttpGet]
        public List<Course> GetFiltered([FromBody] CourseFilter filter)
        {
            return courseController.GetFilteredCourses(filter);
        }

        [HttpPost]
        public Course Post([FromBody] JSONCourse course)
        {
            return courseController.PostCourse(course);
        }
    }
}