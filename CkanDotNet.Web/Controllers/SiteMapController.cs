using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models.Helpers;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class SiteMapController : Controller
    {
        //
        // GET: /SiteMap/
        public ActionResult Index()
        {
            Response.ContentType = "text/xml";
            var urlSet = SiteMapHelper.GetSiteMapUrls(this.ControllerContext);
            return View(urlSet);
        }

    }
}
