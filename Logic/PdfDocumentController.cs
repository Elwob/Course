using Data.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using System;
using System.IO;
using Document = iText.Layout.Document;
using Rectangle = iText.Kernel.Geom.Rectangle;

namespace Logic
{
    internal class PdfDocumentController : MainController
    {
        private DocumentController documentController = new DocumentController();

        public string FillPdf(Person person, EmailTemplate emailTemplate, ref String destFile)
        {
            string docName = documentController.CreateFileName(emailTemplate.DocumentType, person, ".pdf");

            // Use Path class to manipulate file and directory paths. For local Testing !
            // string sourcePath = @"C:\DcvDokumente";
            //  string targetPath = @"C:\DcvDokumente\CopiedVersion";

            ////    System.IO.File.Copy(sourceFile, destFile, false);  For local Testing !
            string folderName = $"{emailTemplate.DocumentType.ToString()}" + ".pdf";
            //    string sourceFile = System.IO.Path.Combine(sourcePath, folderName);
            //    string destFile = String.Format("C:\\DcvDokumente\\CopiedVersion\\{0}.pdf", docName);
            ////  string n = $"{targetPath}"+"\\" +$"{ person.FirstName}" + ".pdf";

            string sourceFile = Path.Combine(templateMainPath, folderName);
            destFile = $"{ documentMainPath}" + "\\" + $"{emailTemplate.DocumentType.ToString()}" + "\\" + docName;

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
            return destFile;
        }
    }
}