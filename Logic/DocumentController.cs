﻿namespace Logic
{
    public class DocumentController
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