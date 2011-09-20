using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models.Helpers;

namespace CkanDotNet.Web.Controllers
{
    public class SiteMapController : Controller
    {
        //
        // GET: /SiteMap/
        [HandleError]
        public ActionResult Index()
        {
            Response.ContentType = "text/xml";
            var urlSet = SiteMapHelper.GetSiteMapUrls(this.ControllerContext);
            return View(urlSet);
        }

    }
}
