using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning subventions
    /// </summary>
    [Route("subvention")]
    [Route("[controller]")]
    [ApiController]
    public class SubventionApiController : ControllerBase
    {
        private SubventionController subventionController = new SubventionController();

        /// <summary>
        /// returns all subventions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Subvention> Get()
        {
            return subventionController.GetAllSubventions();
        }

        /// <summary>
        /// creates a new subvention in DB
        /// </summary>
        /// <param name="recSubvention"></param>
        /// <returns></returns>
        [HttpPost]
        public Subvention Post([FromBody] Subvention recSubvention)
        {
            return subventionController.PostSubvention(recSubvention);
        }

        /// <summary>
        /// updates a certain subvention in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recSubvention"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Subvention Put(int id, [FromBody] Subvention recSubvention)
        {
            return subventionController.PutSubvention(id, recSubvention);
        }

        /// <summary>
        /// deletes a certain subvention in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            subventionController.DeleteSubvention(id);
        }
    }
}