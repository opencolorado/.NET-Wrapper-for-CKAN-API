using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CkanDotNet.Api.Helper
{
    /// <summary>
    /// Represents a cached request/response
    /// </summary>
    public abstract class CachedRequestResponse: CacheEntry
    {
        /// <summary>
        /// Gets or sets the RestRequest associated with this cache entry
        /// </summary>
        public abstract RestRequest Request { get; set; }

        /// <summary>
        /// Gets or sets whether to automatically update this cache entry after expiration
        /// </summary>
        public bool KeepCurrent { get; set; }
    }

    /// <summary>
    /// Represents a cached request/response for a specified resulting type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedRequestResponse<T> : CachedRequestResponse where T : new() 
    {
        /// <summary>
        /// Gets or sets the RestRequest associated with this cache entry
        /// </summary>
        public override RestRequest Request { get; set; }

        /// <summary>
        /// Gets the object that was cached.
        /// </summary>
        public T Data { get; set; }
    }
}
