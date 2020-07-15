using Data.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;
using Document = iText.Layout.Document;
using Person = Data.Models.Person;
using Rectangle = iText.Kernel.Geom.Rectangle;

namespace Logic
{
    public class Email_TemplateController : MainController
    {
        private DocumentController documentController = new DocumentController();
        private PersonController personController = new PersonController();

        public List<Communication> FillDocuments(EmailTemplate emailTemplate)

        {
            List<Communication> communications = new List<Communication>();

            for (int i = 0; i < emailTemplate.PersonIds.Length; i++)
            {
                {
                    //personController.GetPerson();
                    Person person = personController.FindOne(emailTemplate.PersonIds[i]);

                    string docName = documentController.CreateFileName(emailTemplate.DocumentType, person, ".pdf");

                    // Use Path class to manipulate file and directory paths. For Testing !
                    // string sourcePath = @"C:\DcvDokumente";
                    //  string targetPath = @"C:\DcvDokumente\CopiedVersion";

                    ////    System.IO.File.Copy(sourceFile, destFile, false);  For Testing !
                    string folderName = $"{emailTemplate.DocumentType.ToString()}" + ".pdf";
                    //    string sourceFile = System.IO.Path.Combine(sourcePath, folderName);
                    //    string destFile = String.Format("C:\\DcvDokumente\\CopiedVersion\\{0}.pdf", docName);
                    ////  string n = $"{targetPath}"+"\\" +$"{ person.FirstName}" + ".pdf";

                    string sourceFile = System.IO.Path.Combine(templateMainPath, folderName);
                    string destFile = $"{ documentMainPath}" + "\\" + $"{emailTemplate.DocumentType.ToString()}" + "\\" + docName;

                    PdfReader reader = new PdfReader(sourceFile);
                    PdfWriter writer = new PdfWriter(destFile);

                    PdfDocument pdf = new PdfDocument(reader, writer);
                    Document doc = new Document(pdf);
                    Rectangle pagesize;
                    PdfCanvas canvas;
                    int n = pdf.GetNumberOfPages();
                    PdfPage pdfPage = pdf.GetPage(1);
                    canvas = new PdfCanvas(pdfPage);
                    pagesize = pdfPage.GetPageSize();
                    canvas.BeginText().SetFontAndSize(
                        PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN), 12)
                        .MoveText(pagesize.GetWidth() / 2 - 24, pagesize.GetHeight() - 10)
                        .ShowText($"{ person.FirstName.ToString()}" + "," + $"{person.LastName.ToString()}")
                        .EndText();

                    canvas.SaveState();
                    pdf.Close();

                    SendEmail().Wait();

                    Communication communication = documentController.CreateDocumentFromTemplate(emailTemplate, person, null, destFile, docName);
                    communications.Add(communication);
                }
            }
            return communications;
        }

        public async Task SendEmail()
        {
            var apiKey = "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";


                    MailMessage message = new MailMessage("testsenderc@gmail.com", x);
                    message.Sender = new MailAddress("testsenderc@gmail.com");
                    message.Subject = "Using the SmtpClient class.";

                    message.Body = @"Using this feature, you can send an email message from an application very easily.";
                    message.Attachments.Add(new Attachment(destFile));
                    //SmtpClient client = new SmtpClient("smtp.mail.yahoo.com", 465)oder TLS"587"

                    //Todo Remove Testsender !

                    SmtpClient oSmtp = new SmtpClient("smtp.gmail.com");
                    oSmtp.Host = "smtp.gmail.com";
                    oSmtp.Credentials = new NetworkCredential("testsenderc@gmail.com", "Uv8ZDFSWfPQVZ6e");
                    oSmtp.EnableSsl = true;
                    oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSmtp.Port = 587;



                    oSmtp.Send(message);


            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
    }
}