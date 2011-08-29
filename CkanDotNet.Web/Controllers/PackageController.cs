using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Api;
using CkanDotNet.Api.Model;
using CkanDotNet.Web.Models;
using log4net;
using System.Reflection;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Controllers
{
    public class PackageController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //
        // GET: /Package/
        [CheckOffline]
        [HandleError]
        public ActionResult Index(string package)
        {
            log.DebugFormat("Controller action requested for package {0}", package);

            Package packageFound = CkanHelper.GetClient().GetPackage(package, new CacheSettings(SettingsHelper.GetPackageCacheDuration()));
            SettingsHelper.FilterTitle(packageFound);
            SettingsHelper.FilterTags(packageFound.Tags);

            if (package != null)
            {
                this.ConfigureBreadCrumbs(packageFound);
            }

            return View(packageFound);
        }

        /// <summary>
        /// Configure the breadcrumbs for this controller.
        /// </summary>
        /// <param name="package"></param>
        [HandleError]
        private void ConfigureBreadCrumbs(Package package)
        {
            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();

            breadCrumbs.Add(new BreadCrumb(
                "Home",
                "Index",
                "Home"));

            breadCrumbs.Add(new BreadCrumb(
                String.Format("Package > {0}",package.Title)));

            ViewData["BreadCrumbs"] = breadCrumbs;
        }

    }
}
