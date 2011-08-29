using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CkanDotNet.Api.Helper
{
    public class CachedRequestResponse<T> where T : new()
    {
        public RestRequest Request { get; set; }
        public T Data { get; set; }
    }
}
