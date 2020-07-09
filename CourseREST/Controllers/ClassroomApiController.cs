using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseREST.Controllers
{
    [Route("classroom")]
    [Route("[controller]")]
    [ApiController]
    public class ClassroomApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Classroom> get()
        {
            return entities.Classrooms.ToList();
        }
    }
}
