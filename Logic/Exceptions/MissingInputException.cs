using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Exceptions
{
    public class MissingInputException : Exception
    {
        public MissingInputException() : base() { }
        public MissingInputException(string message) : base(message) { }
        public MissingInputException(string message, System.Exception inner) : base(message, inner) { }
    }
}
