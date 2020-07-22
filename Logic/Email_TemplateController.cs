using Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Resources;
using Attachment = System.Net.Mail.Attachment;
using Person = Data.Models.Person;

namespace Logic
{
    public class Email_TemplateController : MainController
    {
        private DocumentController documentController = new DocumentController();
        private PdfDocumentController pdfDocumentController = new PdfDocumentController();
        private PersonController personController = new PersonController();
        private EmailTemplates emailTemplates = new EmailTemplates();

        public EmailTemplate GetEmailTemplate(int id)
        {
            var emailTemplate = (EmailTemplate)entities.EmailTemplates.Where(DocumentId => DocumentId.Equals(id));
            return emailTemplate;
        }

        //Fill Documents and or Email
        public List<Communication> FillDocuments(EmailTemplate emailTemplate)

        {
            List<Communication> communications = new List<Communication>();
            string destFile = null;

            for (int i = 0; i < emailTemplate.PersonIds.Length; i++)
            {
                {
                    //personController.GetPerson();
                    Person person = personController.FindOne(emailTemplate.PersonIds[i]);
                    string docName = emailTemplate.DocumentType.ToString();

                    //only this Id`s get a document attached
                    if ((int)emailTemplate.DocumentType > 1 && (int)emailTemplate.DocumentType < 6)
                    {
                        destFile = pdfDocumentController.FillPdf(person, emailTemplate, ref destFile);
                    }

                    Contact contact = entities.Contacts.Where(x => x.PersonId == person.Id).FirstOrDefault(x => x.ArtOfCommunication.Equals(EChannel.Email));
                    //  var contact = entities.Contacts.Where(x => x.PersonId == person.Id && x.ArtOfCommunication.Equals(EChannel.Email)).ToList();
                    //var contact = entities.Contacts.ToList();

                    // Get Username & Password from Server file
                    var resourceManager = new ResourceManager(typeof(Properties.Resources));
                    var url = resourceManager.GetString("User_Password");
                    string text = File.ReadAllText(url);
                    string[] split = text.Split(";");
                    string userName = split[0];
                    string password = split[1];

                    MailMessage message = new MailMessage(userName, contact.ContactValue);
                    message.Sender = new MailAddress(userName);
                    message.Subject = emailTemplate.DocumentType.ToString();

                    //Call and Fill Email text
                    message = emailTemplates.GetAndFillEmail(ref message, person, emailTemplate);

                    //Connect
                    SmtpClient oSmtp = new SmtpClient("w01959cb.kasserver.com");
                    oSmtp.UseDefaultCredentials = false;
                    oSmtp.Host = "w01959cb.kasserver.com";
                    oSmtp.Credentials = new NetworkCredential(userName, password);
                    oSmtp.EnableSsl = true;
                    oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSmtp.Port = 25;
                    //only this Id`s get a document attached
                    if ((int)emailTemplate.DocumentType > 1 && (int)emailTemplate.DocumentType < 6)
                    {
                        message.Attachments.Add(new Attachment(destFile));
                    }
                    oSmtp.Send(message);

                    Communication communication = documentController.CreateDocumentFromTemplate(emailTemplate, person, null, destFile, docName);
                    communications.Add(communication);
                }
            }
            return communications;
        }
    }
}