using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models;
using log4net;
using System.Reflection;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //
        // GET: /Home/
        [CheckOffline]
        [HandleError]
        public ActionResult Index()
        {
            log.Debug("Controller action requested");

            this.ConfigureBreadCrumbs();
            this.ConfigureMetaTags();

            return View();
        }

        /// <summary>
        /// Prepare the breadcrumbs model for this view.
        /// </summary>
        private void ConfigureBreadCrumbs()
        {
            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();
            breadCrumbs.Add(new BreadCrumb(
                "Home"));
            ViewData["BreadCrumbs"] = breadCrumbs;
        }

        /// <summary>
        /// Prepare the meta tags for this view.
        /// </summary>
        private void ConfigureMetaTags()
        {
            var metaTags = new MetaTags();
            metaTags.Description = SettingsHelper.GetSeoHomeDescription();
            metaTags.Keywords = String.Join(",",SettingsHelper.GetSeoHomeKeywords());
            ViewData["MetaTags"] = metaTags;
        }
    }
}
