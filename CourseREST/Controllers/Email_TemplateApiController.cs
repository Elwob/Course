using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    /// <summary>
    /// contains all requests concerning automated emailtemplates
    /// </summary>
    [Route("EmailTemplate")]
    //  [Route("[controller]")]
    [ApiController]
    public class Email_TemplateApiController : ControllerBase
    {
        private Email_TemplateController email_TemplateController = new Email_TemplateController();

        // TODO: description of method
        [HttpPost]
        public List<Communication> FillDocuments([FromBody] EmailTemplate emailTemplate)
        {
            List<Communication> communications = null;
            try
            {
                communications = email_TemplateController.FillDocuments(emailTemplate);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return communications;  
        }

        /// <summary>
        /// returns a certain EmailTemplate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public EmailTemplate GetEmailTemplate (int id)
        {
            EmailTemplate emailTemplate = null;
            try
            {
                emailTemplate = email_TemplateController.GetEmailTemplate(id);
                Response.StatusCode = 200;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
            return emailTemplate;
        }
    }
}