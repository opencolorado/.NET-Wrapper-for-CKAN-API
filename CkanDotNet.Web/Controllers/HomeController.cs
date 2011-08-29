using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models;
using log4net;
using System.Reflection;

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

            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();
            breadCrumbs.Add(new BreadCrumb(
                "Home"));
            ViewData["BreadCrumbs"] = breadCrumbs;

            return View();
        }

    }
}
