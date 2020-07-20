using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace CourseREST.Controllers
{
    [Route("classroom")]
    [Route("[controller]")]
    [ApiController]
    public class ClassroomApiController : ControllerBase
    {
        private ClassroomController classroomController = new ClassroomController();

        /// <summary>
        /// returns all classrooms existing in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<JSONClassroom> get()
        {
            List<JSONClassroom> classrooms = null;
            try
            {
                classrooms = classroomController.GetRooms();
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return classrooms;
        }
    }
}
