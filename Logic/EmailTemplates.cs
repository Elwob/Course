using Data.Models;
using System;
using System.Linq;
using System.Net.Mail;

namespace Logic
{
    internal class EmailTemplates : MainController
    {
        public MailMessage GetAndFillEmail(ref MailMessage message, Person person, EmailTemplate emailTemplate)
        {
            //     int document template number
            int getDocumentNr = (int)emailTemplate.DocumentType;
            Course course = entities.Courses.FirstOrDefault(id => id.Id == emailTemplate.CourseId);

            // fill database template for diploma Email
            if ((int)emailTemplate.DocumentType == 5)
            {
                String courseName = course.Title;
                EmailTemplate emailTemplateForText = entities.EmailTemplates.FirstOrDefault(id => id.Id == getDocumentNr);
                string body = emailTemplateForText.Text;
                body = body.Replace("{Geschlecht}", person.Gender);
                body = body.Replace("{Vorname}", person.FirstName);
                body = body.Replace("{Nachname}", person.LastName);
                body = body.Replace("{Kurstitel}", courseName);
                message.Body = body;
            }

            //
            if ((int)emailTemplate.DocumentType == 6)
            {
                EmailTemplate emailTemplateForText = entities.EmailTemplates.FirstOrDefault(id => id.Id == getDocumentNr);
                string body = emailTemplateForText.Text;
                String courseName = course.Title;
                DateTime datenow = DateTime.Now;
                DateTime courseTime = course.Start.Value;
                body = body.Replace("{Datenow}", datenow.ToString());
                body = body.Replace("{Vorname}", person.FirstName);
                body = body.Replace("{Nachname}", person.LastName);
                body = body.Replace("{Eventname}", courseName);
                body = body.Replace("{Day}", courseTime.DayOfYear.ToString());
                body = body.Replace("{Time}", courseTime.Hour.ToString());
                //         body = body.Replace("{Street}", course.CourseClassrooms.ToString());
                //        body = body.Replace("{Street}", course.CourseClassrooms.ToString());
            }
            return message;
        }
    }
}