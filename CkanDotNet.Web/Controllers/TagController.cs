using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class TagController : Controller
    {
        /// <summary>
        /// Provides a controller to search by tag.  Redirects to the search controller
        /// and passes the parameter tag.
        /// Get: tag/{tag}
        /// </summary>
        /// <param name="tag">The tag</param>
        /// <returns></returns>
        [CheckOffline]
        public ActionResult Index(string tag)
        {
            return RedirectToAction("Index", "Search", new { tag = tag });
        }

    }
}
