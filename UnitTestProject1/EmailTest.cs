using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Mail;

namespace UnitTestProject1
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            MailMessage message = new MailMessage("testsenderc@gmail.com", "martinus_burtscher@yahoo.de");
            message.Sender = new MailAddress("testsenderc@gmail.com");
            message.Subject = "Using the SmtpClient class.";

            message.Body = @"Using this feature, you can send an email message from an application very easily.";
            //message.Attachments.Add(new Attachment(destFile));
            //SmtpClient client = new SmtpClient("smtp.mail.yahoo.com", 465)587
           
            //Todo Remove Testsender !
         
            SmtpClient oSmtp = new SmtpClient("smtp.gmail.com");
            oSmtp.Host = "smtp.gmail.com";
            oSmtp.Credentials = new NetworkCredential("testsenderc@gmail.com", "Uv8ZDFSWfPQVZ6e");
            oSmtp.EnableSsl = true;
            oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            oSmtp.Port = 587;

   

            oSmtp.Send(message);
        }
    }
}
