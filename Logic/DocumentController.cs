using Data.Models;
using System.Collections.Generic;

namespace Logic
{
    public class DocumentController : MainController
    {

        public static DocumentController instance = null;

        public static DocumentController GetInstance()
        {
            if (instance == null)
            {
                instance = new DocumentController();
            }
            return instance;
        }
    }
}