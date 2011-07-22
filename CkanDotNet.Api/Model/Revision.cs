using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.Helper;

namespace CkanDotNet.Api.Model
{
    public class Revision
    {
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public List<string> Packages { get; set; }
        public string Id { get; set; }
        public string Author { get; set; }

        public DateTime TimestampAsDate
        {
            get
            {
                return DateHelper.Parse(Timestamp);
            }
        }
    }
}
