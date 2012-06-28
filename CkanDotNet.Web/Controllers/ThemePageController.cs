using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models.Helpers;
using CkanDotNet.Web.Models;
using System.IO;

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

            // Get the local path
            string viewPath = Server.MapPath(Url.Content(view));

            if (System.IO.File.Exists(viewPath))
            {
                ViewResult viewResult = View(view);

                // Configure the breadcrumbs
                viewResult.ViewData["BreadCrumbs"] = GetBreadCrumbs(GetViewTitle(viewPath));

                // Return the view
                return viewResult;
            }
            else
            {
                throw new DescriptiveHttpException(404, "Page not found", "The requested page was not found or is no longer available.");
            }
        }

        /// <summary>
        /// Get a title from the view (for example, to use in breadcrumbs)
        /// </summary>
        /// <returns></returns>
        private string GetViewTitle(String path)
        {
            string title = "";

            if (!String.IsNullOrEmpty(path))
            {
                // Get the path to the file with the case that is used on the file system
                string viewPath = Directory.GetFiles(Path.GetDirectoryName(path), Path.GetFileName(path)).FirstOrDefault();

                // Get just the name of the file
                string name = Path.GetFileNameWithoutExtension(viewPath);

                // If the string is Pascal Case, split into words
                // From http://weblogs.asp.net/jgalloway/archive/2005/09/27/426087.aspx
                name = System.Text.RegularExpressions.Regex.Replace(
                    name,
                    "([A-Z])",
                    " $1",
                    System.Text.RegularExpressions.RegexOptions.Compiled).Trim();

                title = name;
            }
            return title;
        }

        /// <summary>
        /// Get the breadcrumbs model for this view.
        /// </summary>
        private BreadCrumbs GetBreadCrumbs(string title)
        {
            BreadCrumbs breadCrumbs = null;

            if (SettingsHelper.GetBreadcrumbsEnabled())
            {
                // Set up the breadcrumbs for this action
                breadCrumbs = new BreadCrumbs();

                breadCrumbs.Add(new BreadCrumb(
                    SettingsHelper.GetCatalogBreadcrumbsBaseLabel(),
                    "Index",
                    "Home"));

                // Add breadcrumb for the search action
                if (!String.IsNullOrEmpty(title))
                {
                    breadCrumbs.Add(new BreadCrumb(
                        title));
                }
            }

            return breadCrumbs;
        }
    }
}