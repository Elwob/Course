
using Data.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Source;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using System;
using System.Collections.Generic;

using Document = iText.Layout.Document;
using Paragraph = DocumentFormat.OpenXml.Drawing.Paragraph;

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
                
                    Person person = personController.FindOne(emailTemplate.PersonIds[i]);

                    // Use Path class to manipulate file and directory paths. For Testing
                    string sourcePath = @"C:\DcvDokumente";
                   //  string targetPath = @"C:\DcvDokumente\CopiedVersion";

                    //    System.IO.File.Copy(sourceFile, destFile, false);
                    string sourceFile = System.IO.Path.Combine(sourcePath, "Diploma.pdf");
                    string destFile = String.Format("C:\\DcvDokumente\\CopiedVersion\\{0}.pdf", person.FirstName);
                    //string n = $"{targetPath}"+"\\" +$"{ person.FirstName}" + ".pdf";
                    //         PdfReader reader = new PdfReader(sourceFile);



                    PdfReader reader = new PdfReader(sourceFile);
                    PdfWriter writer = new PdfWriter(destFile);

                    PdfDocument pdf = new PdfDocument(reader,writer);
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
                       .ShowText("hallöle")
                       .EndText();
                
                    canvas.SaveState();
                    pdf.Close();
                   
                      
              


        //           
               



                    //  PdfMerger merger = new PdfMerger(pdf);
                    //Document doc =new Document(pdf);
                    //   doc.Add(Paragraph("hallöle"));
                    //   doc.Close();
              //      PdfDocument pdf = new PdfDocument();

              //      PdfWriter pdfWriter = new PdfWriter("gugi.pdf",pdf);
                    


                    Communication communication = documentController.CreateDocumentFromTemplate(emailTemplate, person, null, null, null);
                    communications.Add(communication);
                    //                    14 // Add ListItem objects
                    // list.add(new ListItem("Never gonna give you up"))
                    //add(new ListItem("Never gonna let you down"))
                    //add(new ListItem("Never gonna run around and desert you"))
                    //add(new ListItem("Never gonna make you cry"))
                    //add(new ListItem("Never gonna say goodbye"))
                    //add(new ListItem("Never gonna tell a lie and hurt you"));
                    //                    21 // Add the list
                    // document.add(list);
                    //                    23 document.close();


                }
            }
            return communications;
        }

        private AreaBreak Paragraph(string v)
        {
            throw new NotImplementedException();
        }
    }
}