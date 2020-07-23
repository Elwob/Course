using Data.Models;
using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning Courses
    /// </summary>
    [Route("course")]
    [Route("[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private CourseController courseController = new CourseController();

        // not needed anymore, since courses are now returned as JSONCourseSend, which is generated from JSONConverter
        public CourseApiController()
        {
            Data.Models.Content.ShouldIgnoreRelation = true;
            Person.ShouldIgnoreRelation = true;
            Classroom.ShouldIgnoreRelation = true;
        }

        /// <summary>
        /// gets a list of all Courses converted to a JSON format
        /// </summary>
        /// <returns>a list of JSONCourseSends</returns>
        [HttpGet]
        public List<JSONCourseSend> GetAll()
        {
            List<JSONCourseSend> returnList = null;
            try
            {
                returnList = courseController.GetAllCourses();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnList;
        }

        /// <summary>
        /// filters Courses according to the entries at frontend
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>a list of filtered JSONCourseSends</returns>
        [Route("search")]
        [HttpPost]
        public List<JSONCourseSend> Get([FromBody] CourseFilter filter)
        {
            List<JSONCourseSend> returnList = null;
            try
            {
                returnList = courseController.GetFilteredCourses(filter);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnList;
        }

        /// <summary>
        /// creates a new Course in DB
        /// </summary>
        /// <param name="course"></param>
        /// <returns>the created course as JSONCourseSend</returns>
        [HttpPost]
        public JSONCourseSend Post([FromBody] JSONCourseReceive course)
        {
            JSONCourseSend returnCourse = null;
            try
            {
                returnCourse = courseController.PostCourse(course);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnCourse;
        }

        /// <summary>
        /// updates a Course in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="course"></param>
        /// <returns>the updated course as JSONCourseSend</returns>
        [HttpPut("{id}")]
        public JSONCourseSend Put(int id, [FromBody] JSONCourseReceive course)
        {
            JSONCourseSend returnCourse = null;
            try
            {
                returnCourse = courseController.UpdateCourse(id, course);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnCourse;
        }

        /// <summary>
        /// deletes a certain Course in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                courseController.DeleteCourse(id);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
        }
    }
}