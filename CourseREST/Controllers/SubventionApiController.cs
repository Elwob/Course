using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    [Route("subvention")]
    [Route("[controller]")]
    [ApiController]
    public class SubventionApiController : ControllerBase
    {
        private SubventionController subventionController = SubventionController.GetInstance();

        [HttpGet]
        public List<Subvention> Get()
        {
            return subventionController.GetAllSubventions();
        }

        [HttpPost]
        public Subvention Post([FromBody] Subvention recSubvention)
        {
            return subventionController.PostSubvention(recSubvention);
        }

        [HttpPut("{id}")]
        public Subvention Put(int id, [FromBody] Subvention recSubvention)
        {
            return subventionController.PutSubvention(id, recSubvention);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            subventionController.DeleteSubvention(id);
        }
    }
}