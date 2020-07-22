using System;

namespace Logic.Exceptions
{
    public class MissingInputException : Exception
    {
        public MissingInputException() : base()
        {
        }

        public MissingInputException(string message) : base(message)
        {
        }

        public MissingInputException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}