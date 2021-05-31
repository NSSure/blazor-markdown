using System;

namespace Blazor.Markdown.Core.Exceptions
{
    public class MissingRequiredActionException : Exception
    {
        public MissingRequiredActionException()
        {

        }

        public MissingRequiredActionException(string message) : base(message)
        {

        }

        public MissingRequiredActionException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
