using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkanDotNet.Web.Models.Settings
{
    public class ResourceType
    {
        public string Title { get; set; }

        List<ResourceAction> actions = new List<ResourceAction>();

        public List<ResourceAction> Actions
        {
            get
            {
                return actions;
            }
        }
    }
}