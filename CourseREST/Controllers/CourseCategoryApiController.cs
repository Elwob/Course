using System.Collections.Generic;
using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning CourseCategories
    /// </summary>
    [Route("category")]
    [Route("[controller]")]
    [ApiController]
    public class CourseCategoryApiController : ControllerBase
    {
        CourseCategoryController courseCategoryController = new CourseCategoryController();

        [HttpGet]
        public List<CourseCategory> Get()
        {
            return courseCategoryController.GetAllCategories();
        }

        [HttpPost]
        public CourseCategory Post([FromBody] CourseCategory courseCategory)
        {
            return courseCategoryController.PostCategory(courseCategory);
        }

        [HttpPut("{id}")]
        public CourseCategory Put(int id, [FromBody] CourseCategory courseCategory)
        {
            return courseCategoryController.UpdateCategory(id, courseCategory);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            courseCategoryController.DeleteCategory(id);
        }
    }
}
