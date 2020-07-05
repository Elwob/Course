using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("communication")]
    [Route("[controller]")]
    [ApiController]
    public class CommunicationApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Communication> get()
        {
            var communications = entities.Communications.ToList();
            return communications;
        }
    }
}