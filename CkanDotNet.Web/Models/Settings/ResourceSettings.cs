using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkanDotNet.Web.Models.Settings
{
    public class ResourceSettings
    {
        Dictionary<string,ResourceType> types = new Dictionary<string,ResourceType>();

        public Dictionary<string,ResourceType> Types
        {
            get
            {
                return types;
            }
        }
    }
}
