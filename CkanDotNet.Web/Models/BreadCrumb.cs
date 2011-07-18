using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace CkanDotNet.Web.Models
{
    public class BreadCrumb
    {
        public string Label { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public bool Filter { get; set; }

        public BreadCrumb(string label)
        {
            this.Label = label;
        }

        public BreadCrumb(string label, string actionName, string controllerName)
        {
            this.Label = label;
            this.ActionName = actionName;
            this.ControllerName = controllerName;
        }

        public BreadCrumb(string label, string actionName, RouteValueDictionary routeValues, bool filter)
        {
            this.Label = label;
            this.ActionName = actionName;
            this.RouteValues = routeValues;
            this.Filter = filter;
        }

        public bool IsLink
        {
            get
            {
                if (this.ControllerName != null && this.ActionName != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}