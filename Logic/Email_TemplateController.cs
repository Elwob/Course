using Data.Models;
using System;

namespace Logic
{
    public class Email_TemplateController : MainController
    {
        PersonController personController = new PersonController();
        public EmailTemplate FillDocuments(EmailTemplate emailTemplate)
        {
           
            for (int i = 0; i < emailTemplate.PersonIds.Length; i++)
            {

                {
                    string fileName = "text";
                    Person person = personController.FindOne(emailTemplate.PersonIds[i]);
                    string sourcePath = @"C:\DcvDokumente";
                    string targetPath = @"C:\DcvDokumente\CopiedVersion";


                    string destFile=String.Format("C:\\DcvDokumente\\CopiedVersion\\{0}.pdf",person.FirstName);
                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = System.IO.Path.Combine(sourcePath,"Diploma.pdf");
                 //   string destFile = System.IO.Path.Combine(targetPath, "Diploma.pdf");
                    System.IO.File.Copy(sourceFile,destFile,false);
                    // To copy a folder's contents to a new location:
                    // Create a new target folder.
                    // If the directory already exists, this method does not create a new directory.
                    //    System.IO.Directory.CreateDirectory(targetPath);

                    // To copy a file to another location and
                    //  System.IO.File.Copy(sourceFile, destFile, true);

                    // To copy all the files in one directory to another directory.
                    // Get the files in the source folder. (To recursively iterate through
                    // all subfolders under the current directory, see
                    // "How to: Iterate Through a Directory Tree.")
                    // Note: Check for target path was performed previously
                    //       in this code example.
                    //if (System.IO.Directory.Exists(sourcePath))
                    //{
                    //    string[] files = System.IO.Directory.GetFiles(sourcePath);

                    //    // Copy the files and overwrite destination files if they already exist.
                    //    foreach (string s in files)
                    //    {
                    //        // Use static Path methods to extract only the file name from the path.
                    //        fileName = System.IO.Path.GetFileName(s);
                    //        destFile = System.IO.Path.Combine(targetPath, fileName);
                    //        System.IO.File.Copy(s, destFile, true);
                    //    }
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Source path does not exist!");
                    //}

                    //        // Keep console window open in debug mode.
                    //        Console.WriteLine("Press any key to exit.");
                    //        Console.ReadKey();
                }


            }
            return emailTemplate;

        }
    }
}