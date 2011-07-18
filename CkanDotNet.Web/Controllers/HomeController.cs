using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();
            breadCrumbs.Add(new BreadCrumb(
                "Home"));
            ViewData["BreadCrumbs"] = breadCrumbs;

            return View();
        }

    }
}
