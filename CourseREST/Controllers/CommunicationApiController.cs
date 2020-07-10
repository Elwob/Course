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

        [HttpGet("{id}/{className}")]
        public List<JObject> GetVariousCommunications(int id, EClass className)
        {
            var communications = communicationController.GetCommunicationsNeeded(id, className);

            ///that Enums will be shown correctly in JSON
            List<JObject> jsons = DocumentApiController.SerializeAndCreateJsonObject<Communication>(communications);
            return jsons;
        }


        [HttpDelete("{id}")]
        public string DeleteById(int id)
        {
            return communicationController.DeleteById(id);
        }

    }
}