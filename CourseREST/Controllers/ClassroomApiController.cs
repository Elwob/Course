using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning classrooms
    /// </summary>
    [Route("classroom")]
    [Route("[controller]")]
    [ApiController]
    public class ClassroomApiController : ControllerBase
    {
        private RelCourseClassroomController classroomController = new RelCourseClassroomController();

        /// <summary>
        /// gets all classrooms existing in DB
        /// </summary>
        /// <returns>a list of JSONClassrooms</returns>
        [HttpGet]
        public List<JSONClassroom> get()
        {
            List<JSONClassroom> classrooms = null;
            try
            {
                classrooms = classroomController.GetRooms();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return classrooms;
        }
    }
}