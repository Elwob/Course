using System.Collections.Generic;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Data.Models.JSONModels;

namespace CourseREST.Controllers
{
    [Route("classroom")]
    [Route("[controller]")]
    [ApiController]
    public class ClassroomApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        ClassroomController classroomController = new ClassroomController();

        [HttpGet]
        public List<JSONClassroom> get()
        {
            return classroomController.GetRooms();
        }
    }
}
