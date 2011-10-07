using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Helper
{
    public class CacheEntry
    {
        public string Label { get; set; }
        public DateTime LastCached { get; set; }
        public TimeSpan Duration { get; set; }
        public object Data { get; set; }
    }
}
