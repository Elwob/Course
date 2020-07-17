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

        /// <summary>
        /// returns a list of all contents converted to a JSON format
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<JSONCourseSend> GetAll()
        {
            return courseController.GetAllCourses();
        }

        /// <summary>
        /// filters courses according to the entries at frontend
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        public List<JSONCourseSend> Get([FromBody] CourseFilter filter)
        {
            List<JSONCourseSend> myList = null;
            try
            {
                myList = courseController.GetFilteredCourses(filter);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return myList;
        }

        /// <summary>
        /// creates a new course in DB
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public JSONCourseSend Post([FromBody] JSONCourseReceive course)
        {
            return courseController.PostCourse(course);
        }

        /// <summary>
        /// updates a course in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public JSONCourseSend Put(int id, [FromBody] JSONCourseReceive course)
        {
            return courseController.UpdateCourse(id, course);
        }

        /// <summary>
        /// deletes a certain course in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            courseController.DeleteCourse(id);
        }
    }
}