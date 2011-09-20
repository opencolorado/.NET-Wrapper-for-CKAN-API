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
        /// <summary>
        /// Builds a route value dictionary from existing route values and appends querystring values as parameters.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Builds a route value dictionary from existing route values and appends querystring values as parameters.
        /// </summary>
        /// <param name="routeValues"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates a single parameter in a route value dictionary while leaving other route values the same
        /// </summary>
        /// <param name="routeValues"></param>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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