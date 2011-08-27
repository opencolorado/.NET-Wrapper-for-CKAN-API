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
                // Do a server transfer to the offline controller
                ServerTransferToRoute(filterContext,
                    new { controller = "Offline", action = "Index" }
                );

                // End the response
                filterContext.HttpContext.Response.End();
            }
        }

        /// <summary>
        /// Redirect to another route using a server side transfer (so the client URL does not change)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="routeValues"></param>
        private void ServerTransferToRoute(ActionExecutingContext context, object routeValues)
        {
            var httpContextBase = context.HttpContext;

            var rc = new RequestContext(httpContextBase, context.RouteData);
            string url = RouteTable.Routes.GetVirtualPath(rc,
                new RouteValueDictionary(routeValues)).VirtualPath;

            // MVC 3 running on IIS 7+
            if (HttpRuntime.UsingIntegratedPipeline)
            {
                httpContextBase.Server.TransferRequest(url, true);
            }
            else
            {
                // Pre IIS7

                // Get the current application to get the real HttpContext
                var app = (HttpApplication)httpContextBase.GetService(typeof(HttpApplication));

                // Rewrite the path of the request
                httpContextBase.RewritePath(url, false);

                // Process the modified request
                IHttpHandler httpHandler = new MvcHttpHandler();
                httpHandler.ProcessRequest(app.Context);
            }
        }
    }
}