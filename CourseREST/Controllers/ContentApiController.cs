using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("content")]
    [Route("[controller]")]
    [ApiController]
    public class ContentApiController : ControllerBase
    {
        
        [HttpGet]
        public List<Content> Get()
        {
            List<Content> contents = null;
            return contents;
         
        }
    }
}