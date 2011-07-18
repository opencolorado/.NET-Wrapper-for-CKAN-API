using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Model
{
    public class PackageExtras
    {
        public string LevelOfGovernment { get; set; }
        public string Agency { get; set; }
        public string UpdateFrequency { get; set; }
        public string TemporalCoverageFrom { get; set; }
        public string TemporalCoverageTo { get; set; }
    }
}
