﻿using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CourseREST.Controllers
{
    [Route("contact")]
    [Route("[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        private CourseEntities entities = CourseEntities.GetInstance();

        /// <summary>
        /// returns all contacts existing in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Contact> get()
        {
            List<Contact> contacts = null;
            try
            {
                contacts = entities.Contacts.ToList();
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return contacts;
        }
    }
}