using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CkanDotNet.Api.Helper
{
    public class CachedRequestResponse<T>
    {
        public RestRequest Request { get; set; }
        public RestResponse<T> Response { get; set; }
    }
}
