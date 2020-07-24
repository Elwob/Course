using System;

namespace Logic.Exceptions
{
    public class FileDoesNotExistException : Exception
    {
        public FileDoesNotExistException() : base()
        {
        }

        public FileDoesNotExistException(string message) : base(message)
        {
        }

        public FileDoesNotExistException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}