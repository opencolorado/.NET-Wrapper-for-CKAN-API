using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CkanDotNet.Api.Helper
{
    public abstract class CachedRequestResponse: CacheEntry
    {
        public abstract RestRequest Request { get; set; }

        public bool KeepCurrent { get; set; }
    }

    public class CachedRequestResponse<T> : CachedRequestResponse where T : new() 
    {
        public override RestRequest Request { get; set; }
        public T Data { get; set; }
    }
}
