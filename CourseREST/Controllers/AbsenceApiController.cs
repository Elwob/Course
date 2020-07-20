using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning absences
    /// </summary>
    [Route("Absence")]
    [ApiController]
    public class AbsenceApiController : ControllerBase
    {
        private AbsenceController absenceController = new AbsenceController();

        /// <summary>
        /// creates an absence in DB
        /// </summary>
        /// <param name="absence"></param>
        /// <returns></returns>
        [HttpPost]
        public Absence Post(Absence absence)
        {
            Absence returnAbsence = null;
            try
            {
                returnAbsence = absenceController.Post(absence);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return returnAbsence;
        }

        /// <summary>
        /// updates certain absence in DB
        /// </summary>
        /// <param name="absence"></param>
        /// <returns></returns>
        [HttpPut]
        public Absence Put(Absence absence)
        {
            Absence returnAbsence = null;
            try
            {
                returnAbsence = absenceController.Put(absence);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return returnAbsence;
        }

        /// <summary>
        /// finds and returns a certain absence in DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Absence Get(int id)
        {
            Absence absence = null;
            try
            {
                absence = absenceController.Get(id);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return absence;
        }
    }
}