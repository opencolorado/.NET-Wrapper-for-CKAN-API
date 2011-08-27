using System;
using System.Collections.Generic;
using System.Text;

namespace CkanDotNet.Api.Exceptions
{
    public class CkanRequestException : CkanException
    {
        public int HttpStatusCode { get; set; }

        public CkanRequestException(string message, int httpStatusCode)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public CkanRequestException(string message, int httpStatusCode, Exception ex)
            : base(message, ex)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}