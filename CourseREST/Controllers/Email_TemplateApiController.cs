﻿using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace CourseREST.Controllers
{
    [Route("EmailTemplate")]
    //  [Route("[controller]")]
    [ApiController]
    public class Email_TemplateApiController : ControllerBase
    {
        private Email_TemplateController email_TemplateController = new Email_TemplateController();

        [HttpPost]
        public Communication FillDocuments(EmailTemplate emailTemplate)
        {
            var communication =  email_TemplateController.FillDocuments(emailTemplate);
            return communication;
        }
    }
}