using Data.Models;
using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Course> Get([FromBody] CourseFilter filter)
        {
            return courseController.GetFilteredCourses(filter);
        }

        [HttpPost]
        public Course Post([FromBody] JSONCourse course)
        {
            return courseController.PostCourse(course);
        }
    }

    [Route("category")]
    [Route("[controller]")]
    [ApiController]
    public class CourseCategoryApiController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return Enum.GetNames(typeof(ECourseCategory)).ToList();
        }
    }


}