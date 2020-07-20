using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

using System.Collections.Generic;

namespace CourseREST.Controllers
{
    [Route("communication")]
    [Route("[controller]")]
    [ApiController]
    public class CommunicationApiController : ControllerBase
    {
        private CommunicationController communicationController = new CommunicationController();
        /// <summary>
        /// Get Communications from one Person concerning one Course
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="personId"></param>
        /// <returns>List<Communication></Communications></returns>
        [HttpGet("{courseId}/{personId}")]
        public List<Communication> GetVariousCommunications(int courseId, int personId)
        {
            List<Communication> communications = null;
            try
            {
                communications = communicationController.GetCommunicationsNeeded<Course>(courseId, personId);
                Response.StatusCode = 200;
            }
            catch(Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return communications;
        }
        /// <summary>
        /// Delete one Communication by its Id; Assigned Document will not be deleted;
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>

        [HttpDelete("{id}")]
        public string DeleteById(int id)
        {
            string responseString = null;
            try
            {
                responseString = communicationController.DeleteById(id);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }        
            return responseString;
        }
        /// <summary>
        /// Change an existing communication; currently a new comment is expected 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="communication"></param>
        /// <returns>Communication</returns>
        [HttpPut("{id}")]
        public Communication Put(int id, [FromBody] Communication communication)
        {
            Communication changedCommunication = null;
            try
            {
                changedCommunication = communicationController.ChangeCommunication(id, communication);
                Response.StatusCode = 200;
            }
            catch
            {
                Response.StatusCode = 500;
                throw;
            }
            return communication;
        }
        /// <summary>
        /// We get a Communication Post and communicationController is going to handle it
        /// </summary>
        /// <param name="communication"></param>
        /// <returns>Communication</returns>
        [HttpPost]
        public Communication Post([FromBody] Communication communication)
        {
            Communication latestCommunication = null;
            try
            {
                latestCommunication = communicationController.CreateRelationAndAddToDatabase(communication);
                Response.StatusCode = 200;
            }
            catch(Exception)
            {
                Response.StatusCode = 500;
                throw;
            }          
            return latestCommunication;
        }
    }
}