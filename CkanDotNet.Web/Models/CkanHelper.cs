using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Api;

namespace CkanDotNet.Web.Models
{
    public static class CkanHelper
    {
        public static CkanClient GetClient()
        {
            CkanClient client = new CkanClient("colorado.ckan.net");
            return client;
        }
    }
}