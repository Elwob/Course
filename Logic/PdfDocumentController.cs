using Data.Models;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Logic
{
    internal class PdfDocumentController : MainController
    {
        private DocumentController documentController = new DocumentController();

        public string FillPdf(string gender,Person person, Course course, Address address, EmailTemplate emailTemplate, ref String destFile)
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

            //PdfReader pdfReader=
            PdfDocument doc = new PdfDocument(sourceFile);

            DateTime datetime = new DateTime();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("«Anrede»", gender.ToString());
            dictionary.Add("«Titel_vor»", person.Title.ToString());
            dictionary.Add("«Vorname»", person.FirstName.ToString());
            dictionary.Add("«Straße»", address.Street.ToString());
            dictionary.Add("«PLZ»", address.Zip.ToString());
            dictionary.Add("«Ort»", address.Place.ToString());
            dictionary.Add("«Land»", address.Country.ToString());
            dictionary.Add("«Zuname»", person.LastName.ToString());
            dictionary.Add("«Datum»", datetime.ToString());
            dictionary.Add("«KNr_DCV»", course.CourseNumber.ToString());

            //  dictionary.Add("«Kursort»", course.CourseClassrooms.ToString());

            ////fill Document
            //foreach (string dictKey in dictionary.Keys)
            //{
            //    foreach (var item in doc.Pages[0].FindText(dictKey,true).Finds)
            //    {
            //        item.ApplyRecoverString(dictionary[dictKey]);
            //    }
    

            //}

            //foreach ( doc.Pages[0].FindText(dictionary.Keys.ToString()))
            //{
            //}
            //dictionary.Values.ToString()[0][1]

            //for (int i = 0; i < dictionary.Keys.Count; i++)
            //{
            //    if(doc.Pages[0].FindText(dictionary.Values.ToString()[i][1]))

            //   //     var text = doc.Pages[0].FindText("Anrede");

            //}

            //foreach ( dictionary.Keys in doc.Pages[0].FindText(dictionary.))
            //{
            //}

         
            doc.Pages[0].BackgroundColor = Color.White;
            doc.SaveToFile(destFile);

            return destFile;
        }
    }
}