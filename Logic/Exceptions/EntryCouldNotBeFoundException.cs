﻿using System;

namespace Logic.Exceptions
{
    public class EntryCouldNotBeFoundException : Exception
    {
        public EntryCouldNotBeFoundException() : base()
        {
        }

        public EntryCouldNotBeFoundException(string message) : base(message)
        {
        }

        public EntryCouldNotBeFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}