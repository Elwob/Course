using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
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
            var communications =  email_TemplateController.FillDocuments(emailTemplate);
            return communications;
        }
    }
}