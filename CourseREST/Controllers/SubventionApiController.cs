using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseREST.Controllers
{
    [Route("subvention")]
    [Route("[controller]")]
    [ApiController]
    public class SubventionApiController : ControllerBase
    {
        SubventionController subventionController = new SubventionController();

        [HttpGet]
        public List<Subvention> Get()
        {
            return subventionController.GetAllSubventions();
        }

        [HttpPost]
        public Subvention Post([FromBody] ReceivedSubvention recSubvention)
        {
            return subventionController.PostSubvention(recSubvention);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ReceivedSubvention recSubvention)
        {
            subventionController.PutSubvention(id, recSubvention);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            subventionController.DeleteSubvention(id);
        }
    }
}
