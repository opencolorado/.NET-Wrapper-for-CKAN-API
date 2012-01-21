using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using System.Reflection;
using CkanDotNet.Api;
using CkanDotNet.Web.Models;
using CkanDotNet.Web.Models.Helpers;
using System.Net;

namespace CkanDotNet.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Error",
                "error",
                new { controller = "Error", action = "Index" }
            );

            routes.MapRoute(
                "SiteMap", // Route name
                "sitemap.xml", // URL with parameters
                new { controller = "SiteMap", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Suggest", // Route name
                "suggest/{prefix}", // URL with parameters
                new { controller = "Suggestion", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Download Proxy", // Route name
                "download/{*path}", // URL with parameters
                new { controller = "DownloadProxy", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Tag", // Route name
                "tag/{tag}", // URL with parameters
                new { controller = "Tag", action = "Index", tag = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Dataset", // Route name
                "dataset/{package}", // URL with parameters
                new { controller = "Package", action = "Index", package = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Cache", // Route name
                "admin/cache/{action}", // URL with parameters
                new { controller = "Cache", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Search", // Route name
                "search", // URL with parameters
                new { controller = "Search", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Home", // Route name
                "{controller}", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Widget", // Route name
                "widget/{action}", // URL with parameters
                new { controller = "Widget", action = "Index" } // Parameter defaults
            );

        }

        /// <summary>
        /// Handle Application_Start event
        /// </summary>
        protected void Application_Start()
        {
            // Configure log4net
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Start caching of auto-cached CKAN requests if we aren't in offline mode
            try
            {
                if (!SettingsHelper.GetOfflineEnabled())
                {
                    if (SettingsHelper.GetPackageCountCacheBackgroundUpdate())
                    {
                        CkanHelper.GetPackageCount();
                    }

                    if (SettingsHelper.GetAllPackagesCacheBackgroundUpdate())
                    {
                        CkanHelper.GetAllPackages();
                    }

                    if (SettingsHelper.GetAllLicensesCacheBackgroundUpdate())
                    {
                        CkanHelper.GetLicenses();
                    }
                }
            }
            catch (Exception ex)
            {
                // If any errors occur while trying to start caching, log this and clear the error.
                // Any new page request will re-attempt to start the caching process
                log.Error("Unable to starting background caching on application start", ex);
                Server.ClearError();
            }
        }

        /// <summary>
        /// Handle application level errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            // Get the last error and clear the response
            Exception ex = Server.GetLastError();
            Response.Clear();

            var errorPresentation = ErrorHelper.GetDefaultErrorPresentation(ex);

            // Set the status code on the response
            Response.StatusCode = errorPresentation.StatusCode;

            if (errorPresentation.StatusCode >= 500)
            {
                log.Error("An unhandled server error has occurred", ex);
            }
            else
            {
                log.Debug("An unhandled error has occurred", ex);
            }

            // Display the error view to the client
            ErrorHelper.DisplayErrorView(errorPresentation);
        }

    }
}