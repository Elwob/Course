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
            Person.ShouldIgnoreRelation = true;
            Classroom.ShouldIgnoreRelation = true;
        }

        [HttpGet]
        public List<JSONCourseSend> GetAll()
        {
            return courseController.GetAllCourses();
        }

        [Route("search")]
        [HttpPost]
        public List<JSONCourseSend> Get([FromBody] CourseFilter filter)
        {
            return courseController.GetFilteredCourses(filter);
        }

        [HttpPost]
        public Course Post([FromBody] JSONCourseReceive course)
        {
            return courseController.PostCourse(course);
        }

        //[HttpPut("{id}")]
        //public Course

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            courseController.DeleteCourse(id);
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