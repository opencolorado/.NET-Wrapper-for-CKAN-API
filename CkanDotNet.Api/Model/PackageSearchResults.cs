using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class PackageSearchResults
    {
        public int Count { get; set; }
        public List<Package> Results { get; set; }

        public PackageSearchResults()
        {
            Results = new List<Package>();
        }
    }
}
