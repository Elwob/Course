
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logic
{
    public class DocumentController
    {
        public static DocumentController instance = null;

        public static DocumentController GetInstance()
        {
            if(instance == null)
            {
                instance = new DocumentController();
            }
            return instance;
        }

   
    }
}
