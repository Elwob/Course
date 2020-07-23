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
                String courseName = course.Title;
                EmailTemplate emailTemplateForText = entities.EmailTemplates.FirstOrDefault(id => id.Id == getDocumentNr);
            DateTime datenow = DateTime.Now;
            DateTime courseTime = course.Start.Value;
         
            string body = emailTemplateForText.Text;

                body = body.Replace("{Geschlecht}", person.Gender);
                body = body.Replace("{Vorname}", person.FirstName);
                body = body.Replace("{Nachname}", person.LastName);
                body = body.Replace("{Kurstitel}", courseName);
                body = body.Replace("{Datenow}", datenow.ToString());
                body = body.Replace("{Eventname}", courseName);
                body = body.Replace("{Day}", courseTime.DayOfYear.ToString());
                body = body.Replace("{Time}", courseTime.Hour.ToString());
                body = body.Replace("{Month}", courseTime.Month.ToString());
                body = body.Replace("{Street}", course.CourseClassrooms.ToString());
            message.Body = body;
        
            return message;
        }
    }
}