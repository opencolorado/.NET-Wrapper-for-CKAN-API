using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models.Helpers;
using System.Web.Routing;

namespace CkanDotNet.Web.Models
{
    public class CheckOfflineAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext); 
            
            if (SettingsHelper.IsDataCatalogOffline())
            {
                RedirectToRoute(filterContext,
                    new { controller = "Offline", action = "Index" }
                );
            }
        }

        private void RedirectToRoute(ActionExecutingContext context, object routeValues)
        {
            var rc = new RequestContext(context.HttpContext, context.RouteData);
            string url = RouteTable.Routes.GetVirtualPath(rc,
                new RouteValueDictionary(routeValues)).VirtualPath;
            context.HttpContext.Response.Redirect(url, true);
        }
    }
}