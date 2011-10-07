using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CkanDotNet.Api.Helper
{
    public class CacheEntrySummary : IComparable
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public DateTime LastCached { get; set; }
        public TimeSpan Duration { get; set; }
        public bool KeepCurrent { get; set; }
        public DateTime Expires
        {
            get
            {
                return LastCached + Duration;
            }
        }

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
 