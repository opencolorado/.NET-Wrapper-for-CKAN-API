using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkanDotNet.Web.Models.Settings
{
    public class ResourceAction
    {
        public string Action { get; set; }
        public string UrlTemplate { get; set; }
        public string Title { get; set; }

        public string BuildUrl(string url)
        {
            if (!String.IsNullOrEmpty(UrlTemplate) && UrlTemplate.Contains("{url}"))
            {
                url = UrlTemplate.Replace("{url}", url);
            }
            return url;
        }
    }
}