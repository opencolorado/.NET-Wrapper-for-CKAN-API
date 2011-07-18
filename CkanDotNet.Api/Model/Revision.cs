using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class Revision
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public List<string> Packages { get; set; }
        public string Id { get; set; }
        public string Author { get; set; }
    }
}
