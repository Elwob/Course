using Data.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        private CourseCategoryController courseCategoryController = new CourseCategoryController();

        /// <summary>
        /// gets all CourseCategories existing in DB
        /// </summary>
        /// <returns>a list of CourseCategories</returns>
        [HttpGet]
        public List<CourseCategory> Get()
        {
            List<CourseCategory> returnList = null;
            try
            {
                returnList = courseCategoryController.GetAllCategories();
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return returnList;
        }

        /// <summary>
        /// creates a new CourseCategory in DB
        /// </summary>
        /// <param name="courseCategory"></param>
        /// <returns>the created CourseCategory</returns>
        [HttpPost]
        public CourseCategory Post([FromBody] CourseCategory courseCategory)
        {
            CourseCategory category = null;
            try
            {
                category = courseCategoryController.PostCategory(courseCategory);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return category;
        }

        /// <summary>
        /// updates a certain CourseCategory in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseCategory"></param>
        /// <returns>the updated CourseCategory</returns>
        [HttpPut("{id}")]
        public CourseCategory Put(int id, [FromBody] CourseCategory courseCategory)
        {
            CourseCategory category = null;
            try
            {
                category = courseCategoryController.UpdateCategory(id, courseCategory);
                Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
            }
            return category;
        }

        /// <summary>
        /// deletes a certain CourseCategory in DB
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                courseCategoryController.DeleteCategory(id);
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