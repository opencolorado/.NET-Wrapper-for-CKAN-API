using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class ResourceSearchResponse
    {
        public int Count { get; set; }
        public List<Resource> Results { get; set; }

        public ResourceSearchResponse()
        {
            Results = new List<Resource>();
        }
    }
}
