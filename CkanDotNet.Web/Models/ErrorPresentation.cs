using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Web.Models
{
    public class ErrorPresentation
    {
        public int StatusCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Exception Exception { get; set; }

        public ErrorPresentation()
        {
            // Set the default status code
            StatusCode = 500;
        }
    }
}
