
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
                    string fileName = "text";
                    Person person = personController.FindOne(emailTemplate.PersonIds[i]);
                    string sourcePath = @"C:\DcvDokumente";
                    string targetPath = @"C:\DcvDokumente\CopiedVersion";

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = "\\" + templateMainPath + "\\TestDocument.pdf";



                    //PdfReader reader = new PdfReader(sourceFile);
                    //PdfWriter writer = new PdfWriter(targetPath);

                    //PdfDocument pdf = new PdfDocument(reader, writer);
                    //Document doc = new Document(pdf);
                    //PageSize pageSize = new PageSize(210, 297);
                    //var area = doc.GetPageEffectiveArea(pageSize);

                 



                    Communication communication = documentController.CreateDocumentFromTemplate(emailTemplate, person, null, null, null);
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