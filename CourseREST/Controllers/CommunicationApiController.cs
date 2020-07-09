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
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet("{id}/{className}")]
        public List<JObject> GetVariousCommunications(int id, EClass className)
        {
            var communications = communicationController.GetCommunicationsNeeded(id, className);

            ///that Enums will be shown correctly in JSON
            List<JObject> jsons = DocumentApiController.SerializeAndCreateJsonObject<Communication>(communications);

            return jsons;
        }
    }
}