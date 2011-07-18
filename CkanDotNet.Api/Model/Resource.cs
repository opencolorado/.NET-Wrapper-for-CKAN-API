using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class Resource
    {
        public string Id { get; set; }
        public string ResourceGroupId { get; set; }
        public string Url { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public string Hash { get; set; }
        public string Position { get; set; }
        public string PackageId { get; set; }
    }
}
