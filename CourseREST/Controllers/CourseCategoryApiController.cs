using System;
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

        /// <summary>
        /// returns all course categories existing in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CourseCategory> Get()
        {
            List<CourseCategory> returnList = null;
            try
            {
                returnList = courseCategoryController.GetAllCategories();
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return returnList;
        }

        /// <summary>
        /// creates a new category in DB
        /// </summary>
        /// <param name="courseCategory"></param>
        /// <returns></returns>
        [HttpPost]
        public CourseCategory Post([FromBody] CourseCategory courseCategory)
        {
            CourseCategory category = null;
            try
            {
                category = courseCategoryController.PostCategory(courseCategory);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return category;
        }

        /// <summary>
        /// updates a certain category in DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseCategory"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public CourseCategory Put(int id, [FromBody] CourseCategory courseCategory)
        {
            CourseCategory category = null;
            try
            {
                category = courseCategoryController.UpdateCategory(id, courseCategory);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return category;
        }

        /// <summary>
        /// deletes a certain category in DB
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
            catch (Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
