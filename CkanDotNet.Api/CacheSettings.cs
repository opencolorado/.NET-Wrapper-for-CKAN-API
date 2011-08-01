using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api
{
    public class CacheSettings
    {
        public TimeSpan Duration { get; set; }

        public CacheSettings(TimeSpan duration)
        {
            Duration = duration;
        }
    }
}
