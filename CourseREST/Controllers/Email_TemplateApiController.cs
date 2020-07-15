using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CourseREST.Controllers
{
    [Route("EmailTemplate")]
    //  [Route("[controller]")]
    [ApiController]
    public class Email_TemplateApiController : ControllerBase
    {
        private Email_TemplateController email_TemplateController = new Email_TemplateController();

        [HttpPost]
        public List<Communication> FillDocuments(EmailTemplate emailTemplate)
        {
            var communications = email_TemplateController.FillDocuments(emailTemplate);

            return communications;
            
        }
        [HttpGet]
        public EmailTemplate GetEmailTemplate (int id)
        {
            var emailTemplate = email_TemplateController.GetEmailTemplate( id);
        return emailTemplate;
        }
    }
}