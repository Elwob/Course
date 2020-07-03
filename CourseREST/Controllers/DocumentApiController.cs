using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        [HttpGet]
        public List<Document> get()
        {
            var documents = entities.Documents.ToList();
            return documents;
        }
    }
}
