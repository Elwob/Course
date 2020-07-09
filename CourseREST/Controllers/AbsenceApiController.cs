using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CourseREST.Controllers
{
    [Route("Absence")]
    [ApiController]
    public class AbsenceApiController : ControllerBase
    {
        private AbsenceController absenceController = new AbsenceController();

        [HttpPost]
        public Absence Post(Absence absence)
        {
            absence = absenceController.Post(absence);
            return absence;
        }

        [HttpPut]
        public Absence Put(Absence absence)
        {
            absence = absenceController.Put(absence);
            return absence;
        }

        [HttpGet("{id}")]
        public Absence Get(int id)
        {
            Absence absence = absenceController.Get(id);
            return absence;
        }
    }
}