using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning persons
    /// </summary>
    [Route("person")]
    [Route("[controller]")]
    [ApiController]
    public class PersonApiController : ControllerBase
    {
        public PersonController personController = new PersonController();

        /// <summary>
        /// returns a list of all persons in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Person> Get()
        {
            List<Person> persons = null;
            try
            {
                persons = personController.FindAll();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return persons;
        }

        /// <summary>
        /// returns a list of participants of a certain course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Route("getParticipants/{courseId}")]
        [HttpGet]
        public List<Person> GetParticipants(int courseId)
        {
            List<Person> participants = null;
            try
            {
                participants = personController.FindAllParticipantsOfOneCourse(courseId);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return participants;
        }
    }

    /// <summary>
    /// returns all trainers
    /// </summary>
    [Route("trainer")]
    [Route("[controller]")]
    [ApiController]
    public class TrainerApiController : ControllerBase
    {
        public PersonController personController = new PersonController();

        [HttpGet]
        public List<Person> Get()
        {
            List<Person> trainers = null;
            try
            {
                trainers = personController.FindAllTrainers();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return trainers;
        }
    }
}