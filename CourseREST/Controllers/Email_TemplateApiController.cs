using Data.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace CourseREST.Controllers
{
    [Route("EmailTemplate")]
  //  [Route("[controller]")]
    [ApiController]
    public class Email_TemplateApiController : ControllerBase
    {
        private Email_TemplateController email_TemplateController = new Email_TemplateController();

       [HttpPost]
        public EmailTemplate FillDocuments(EmailTemplate emailTemplate )
        {
            var lullu =  email_TemplateController.FillDocuments(emailTemplate);
            return lullu;
        }
    }
}