using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CkanDotNet.Api.Tests.Helpers
{
    public static class CkanApiHelper
    {
        private static string host = "colorado.ckan.net";

        public static CkanClient GetCkanClient()
        {
            return new CkanClient(host);
        }
    } 
}
