using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseREST.Controllers
{
    [Route("contact")]
    [Route("[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        [HttpGet]
        public List<Contact> get()
        {
            var contacts = entities.Contacts.ToList();
            return contacts;
        }
    }
}
