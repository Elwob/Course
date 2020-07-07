using Data.Models;
using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    [Route("course")]
    [Route("[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private CourseController courseController = CourseController.GetInstance();

        //[HttpGet]
        //public List<Course> getAll()
        //{
        //    return courseController.GetAll();
        //}

        [HttpGet]
        public List<Course> getFiltered([FromBody] CourseFilter filter)
        {
            return courseController.GetFilteredCourses(filter);
        }
    }
}