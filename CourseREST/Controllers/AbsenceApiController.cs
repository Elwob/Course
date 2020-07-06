using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CourseREST.Controllers
{
    [Route("Absence")]
    [Route("[controller]")]
    [Route("Api")]
    public class AbsenceApiController : ControllerBase
    {
        private CourseEntities entitie = CourseEntities.GetInstance();

        [HttpGet]
        public List<Absence> Get()
        {
            List<Absence> absence = entitie.Absences.ToList();
            return absence;
        }
    }
}