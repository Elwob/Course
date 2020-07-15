using Data.Entities;
using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("communication")]
    [Route("[controller]")]
    [ApiController]
    public class CommunicationApiController : ControllerBase
    {
        private CommunicationController communicationController = new CommunicationController();

        [HttpGet("{courseId}/{personId}")]
        public List<Communication> GetVariousCommunications(int courseId, int personId)
        {
            var communications = communicationController.GetCommunicationsNeeded<Course>(courseId, personId);

            return communications;
        }


        [HttpDelete("{id}")]
        public string DeleteById(int id)
        {
            return communicationController.DeleteById(id);
        }

        [HttpPut("{id}")]
        public Communication Put(int id, [FromBody] Communication communication)
        {
            return communicationController.ChangeCommunication(id, communication);
        }

        [HttpPost]
        public Communication Post([FromBody] Communication communication)
        {
            Communication latestCommunication = communicationController.CreateRelationAndAddToDatabase(communication);
            return latestCommunication;
        }

    }
}