using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Helper
{
    /// <summary>
    /// Represents an entry in the cache
    /// </summary>
    public class CacheEntry
    {
        /// <summary>
        /// Gets or sets the label used to identify the cache entry
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the time that the entry was added to the cache
        /// </summary>
        public DateTime LastCached { get; set; }

        /// <summary>
        /// Gets or sets the duration that the entry was cached
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the data being cached
        /// </summary>
        public object Data { get; set; }
    }
}
