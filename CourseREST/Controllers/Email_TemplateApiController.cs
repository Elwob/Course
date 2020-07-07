using Logic;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CourseREST.Controllers
{
    [Route("EmailTemplate")]
    [ApiController]
    public class Email_TemplateApiController : ControllerBase
    {
        private Email_TemplateController email_TemplateController = new Email_TemplateController();

        [HttpPost]
        public void FillDocuments(Email_TemplateController email_TemplateController)
        {
            email_TemplateController.FillDocuments(email_TemplateController);
        }
    }
}