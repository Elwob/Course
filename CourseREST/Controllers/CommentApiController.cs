using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning comments
    /// </summary>
    [Route("comment")]
    [Route("[controller]")]
    [ApiController]
    public class CommentApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        /// <summary>
        /// returns a list of all comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Comment> get()
        {
            List<Comment> comments = null;
            try
            {
                comments = entities.Comments.ToList();
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return comments;
        }
    }
}