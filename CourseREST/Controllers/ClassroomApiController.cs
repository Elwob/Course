using Data.Entities;
using Data.Models.JSONModels;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    [Route("classroom")]
    [Route("[controller]")]
    [ApiController]
    public class ClassroomApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        private ClassroomController classroomController = new ClassroomController();

        [HttpGet]
        public List<JSONClassroom> get()
        {
            return classroomController.GetRooms();
        }
    }
}