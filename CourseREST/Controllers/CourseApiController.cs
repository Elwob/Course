using Data.Models;
using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning courses
    /// </summary>
    [Route("course")]
    [Route("[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private CourseController courseController = new CourseController();

        public CourseApiController()
        {
            Data.Models.Content.ShouldIgnoreRelation = true;
        }

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

    /// <summary>
    /// contains all requests concerning CourseCategories
    /// </summary>
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