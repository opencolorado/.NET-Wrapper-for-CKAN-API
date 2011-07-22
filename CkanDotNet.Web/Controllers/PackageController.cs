using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Api;
using CkanDotNet.Api.Model;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class PackageController : Controller
    {
        //
        // GET: /Package/

        public ActionResult Index(string package)
        {
            Package packageFound = CkanHelper.GetClient().GetPackage(package);

            if (package != null)
            {
                this.ConfigureBreadCrumbs(packageFound);
            }

            return View(packageFound);
        }

        private void ConfigureBreadCrumbs(Package package)
        {
            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();

            breadCrumbs.Add(new BreadCrumb(
                "Home",
                "Index",
                "Home"));

            breadCrumbs.Add(new BreadCrumb(
                String.Format("Package: {0}",package.BaseTitle)));

            ViewData["BreadCrumbs"] = breadCrumbs;
        }

    }
}
