using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
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
