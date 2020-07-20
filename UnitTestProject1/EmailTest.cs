using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Resources;

namespace UnitTestProject1
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string urlTo = ""; //please enter target address for Email Test
            var resourceManager = new ResourceManager(typeof(Properties.Resources));
            var url = resourceManager.GetString("User_Password");
            string text = File.ReadAllText(url);
            string[] split = text.Split(";");
            string userName = split[0];
            string password = split[1];

            MailMessage message = new MailMessage(userName, urlTo);
            message.Sender = new MailAddress(userName);
            message.Subject = "test";

            message.Subject = "Using the SmtpClient class.";
            message.Body = @"Using this feature, you can send an email message from an application very easily.";
            SmtpClient oSmtp = new SmtpClient("w01959cb.kasserver.com");
            oSmtp.UseDefaultCredentials = false;
            oSmtp.Host = "w01959cb.kasserver.com";
            oSmtp.Credentials = new NetworkCredential(userName, password);
            oSmtp.EnableSsl = true;
            oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            oSmtp.Port = 25;
            oSmtp.Send(message);
        }
    }
}