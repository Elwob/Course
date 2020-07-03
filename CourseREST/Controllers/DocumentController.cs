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
    public class DocumentController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();
        [HttpGet]
        public List<Document> getDocuments()
        {
            var documents = entities.Documents.Include(c => c.DocumentClasses).ThenInclude(x => x.Id).ToList();
            return documents;
        }
    }
}
