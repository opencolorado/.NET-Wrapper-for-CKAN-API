using System;
using System.Collections.Generic;
using System.Text;

namespace CkanDotNet.Api.Exceptions
{
    public class CkanException : Exception
    {
        public CkanException(string message)
            : base(message)
        {
        }

        public CkanException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}