using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning addresses
    /// </summary>
    [Route("address")]
    [Route("[controller]")]
    [ApiController]
    public class AddressApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        /// <summary>
        /// returns a list of all addresses in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Address> get()
        {
            List<Address> addresses = null;
            try
            {
                addresses = entities.Addresses.ToList();
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return addresses;
        }
    }
}