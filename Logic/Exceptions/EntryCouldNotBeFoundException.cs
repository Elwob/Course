using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Exceptions
{
    public class EntryCouldNotBeFoundException : Exception
    {
        public EntryCouldNotBeFoundException() : base() { }
        public EntryCouldNotBeFoundException(string message) : base(message) { }
        public EntryCouldNotBeFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}
