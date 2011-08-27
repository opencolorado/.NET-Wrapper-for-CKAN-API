using System;
using System.Collections.Generic;
using System.Text;

namespace CkanDotNet.Api.Exceptions
{
    public class CkanTimeoutException : CkanException
    {
        public CkanTimeoutException(string message)
            : base(message)
        {
        }

        public CkanTimeoutException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}