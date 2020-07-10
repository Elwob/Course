
using iText.IO.Font.Constants;
using Data.Models;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.IO.Font;
using iText.IO.Source;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;
using System;
using System.Collections.Generic;

using Document = iText.Layout.Document;
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

                
                    Communication communication = documentController.CreateDocumentFromTemplate(emailTemplate, person, null,destFile,docName);
                    communications.Add(communication);
            
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