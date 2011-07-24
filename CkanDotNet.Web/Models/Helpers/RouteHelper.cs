using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace CkanDotNet.Web.Models
{
    public static class RouteHelper
    {
        public static RouteValueDictionary RouteFromParameters(ViewContext context)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(context.RouteData.Values);

            NameValueCollection queryString = context.HttpContext.Request.QueryString;

            foreach (string key in queryString.Cast<string>())
            {
                routeValues[key] = queryString[key];
            }

            return routeValues;
        }

        public static RouteValueDictionary RouteFromParameters(RouteValueDictionary routeValues)
        {
            // Clone the initial route values
            routeValues = new RouteValueDictionary(routeValues);

            NameValueCollection queryString = HttpContext.Current.Request.QueryString;

            foreach (string key in queryString.Cast<string>())
            {
                routeValues[key] = queryString[key];
            }

            return routeValues;
        }

        public static RouteValueDictionary UpdateRoute(RouteValueDictionary routeValues, string parameter, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                routeValues.Remove(parameter);
            }
            else
            {
                routeValues[parameter] = value;
            }
            
            return routeValues;
        }
    }
}