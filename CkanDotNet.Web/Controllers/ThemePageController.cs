using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models.Helpers;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class ThemePageController : Controller
    {
        //
        // GET: /ThemePage/

        public ActionResult Index(string page)
        {
            // Check if the theme provides a page with the page name
           string view = "~/Views/Theme/" + SettingsHelper.GetCatalogTheme() + "/" + page + ".cshtml";

           if (System.IO.File.Exists(view))
           {
               return View(view);
           }
           else
           {
               throw new DescriptiveHttpException(404, "Page not found", "The requested page was not found or is no longer available.");
           }
        }

    }
}
