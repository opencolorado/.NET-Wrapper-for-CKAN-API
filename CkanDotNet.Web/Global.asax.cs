using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CkanDotNet.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Tag", // Route name
            //    "tag/{tag}", // URL with parameters
            //    new { controller = "Search", action = "Index", tag = UrlParameter.Optional } // Parameter defaults
            //);

            routes.MapRoute(
                "Package", // Route name
                "package/{package}", // URL with parameters
                new { controller = "Package", action = "Index", package = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Search", // Route name
                "search", // URL with parameters
                new { controller = "Search", action = "Index"} // Parameter defaults
            );

            routes.MapRoute(
                "Home", // Route name
                "{controller}", // URL with parameters
                new { controller = "Home", action = "Index"} // Parameter defaults
            );

            routes.MapRoute(
                "Widget", // Route name
                "widget/{action}", // URL with parameters
                new { controller = "Widget", action = "Index" } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}