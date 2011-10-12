using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CkanDotNet.Api.Helper
{
    public class CacheEntrySummary : IComparable
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cache entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the label used to identify the cache entry
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the Url of the cached request (if applicable)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the time that the entry was cached
        /// </summary>
        public DateTime LastCached { get; set; }

        /// <summary>
        /// Gets or sets the duration that the entry was cached
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets whether to automatically update this cache entry after expiration
        /// </summary>
        public bool KeepCurrent { get; set; }

        /// <summary>
        /// Gets the time that the entry expires
        /// </summary>
        public DateTime Expires
        {
            get
            {
                return LastCached + Duration;
            }
        }

        /// <summary>
        /// Compares this entry to another entry for sorting
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var other = (CacheEntrySummary)obj;

            var order = 0;

            // Sort by keep current
            if (order == 0)
            {
                order = other.KeepCurrent.CompareTo(this.KeepCurrent);
            }

            // Sort by duration descending
            if (order == 0) {
                order = other.Duration.CompareTo(this.Duration);
            }

            // Then sort by expiration ascending
            if (order == 0)
            {
                order = other.Expires.CompareTo(this.Expires);
            }

            return order;
        }
    }
}
 