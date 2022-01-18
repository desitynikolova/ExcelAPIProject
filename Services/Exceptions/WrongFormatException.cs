using System;

namespace Services.Exceptions
{
    public class WrongFormatException : Exception
    {
        private const string EXCEPTION_MESSAGE = "This format is unreadable!";

        public WrongFormatException() : base(EXCEPTION_MESSAGE)
        {

        }

        public WrongFormatException(string message)
           : base(message)
        {

        }
    }
}
