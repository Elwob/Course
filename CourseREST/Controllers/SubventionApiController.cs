using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning Subventions
    /// </summary>
    [Route("subvention")]
    [Route("[controller]")]
    [ApiController]
    public class SubventionApiController : ControllerBase
    {
        private SubventionController subventionController = new SubventionController();

        /// <summary>
        /// gets all Subventions
        /// </summary>
        /// <returns>a list of Subventions</returns>
        [HttpGet]
        public List<Subvention> Get()
        {
            List<Subvention> subventionList = null;
            try
            {
                subventionList = subventionController.GetAllSubventions();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return subventionList;
        }

        /// <summary>
        /// creates a new Subvention in DB
        /// </summary>
        /// <param name="recSubvention"></param>
        /// <returns>the created Subvention</returns>
        [HttpPost]
        public Subvention Post([FromBody] Subvention recSubvention)
        {
            Subvention returnSubvention = null;
            try
            {
                returnSubvention = subventionController.PostSubvention(recSubvention);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnSubvention;
        }

        /// <summary>
        /// updates a certain Subvention in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recSubvention"></param>
        /// <returns>the updated Subvention</returns>
        [HttpPut("{id}")]
        public Subvention Put(int id, [FromBody] Subvention recSubvention)
        {
            Subvention returnSubvention = null;
            try
            {
                returnSubvention = subventionController.PutSubvention(id, recSubvention);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnSubvention;
        }

        /// <summary>
        /// deletes a certain Subvention in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                subventionController.DeleteSubvention(id);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
        }
    }
}