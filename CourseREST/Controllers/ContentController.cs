using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        CourseEntities entities = new CourseEntities();
        [HttpGet]
        public List<Content> get()
        {
            var lala = entities.Content.ToList();
            return lala;
        }
    }
}
