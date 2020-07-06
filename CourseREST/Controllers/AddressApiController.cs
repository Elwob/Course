using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("address")]
    [Route("[controller]")]
    [ApiController]
    public class AddressApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        [HttpGet]
        public List<Address> get()
        {
            var addresses = entities.Addresses.ToList();
            return addresses;
        }
    }
}