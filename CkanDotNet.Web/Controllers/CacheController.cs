using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Api;
using CkanDotNet.Web.Models;
using CkanDotNet.Web.Models.Helpers;
using System.Security;

namespace CkanDotNet.Web.Controllers
{
    public class CacheController : Controller
    {
        //
        // GET: /Cache/
        public ActionResult Index(string token)
        {
            CheckToken(token);

            CkanClient client = CkanHelper.GetClient();
            var cachedItems = client.GetCachedRequests();

            // Set up the breadcrumbs for this action
            var breadCrumbs = new BreadCrumbs();
            breadCrumbs.Add(new BreadCrumb("Home", "Index", "Home"));
            breadCrumbs.Add(new BreadCrumb("Admin"));
            breadCrumbs.Add(new BreadCrumb("Cache"));
            ViewData["BreadCrumbs"] = breadCrumbs;

            // Disable analytics on admin pages
            ViewData["DisableAnalytics"] = true;

            return View(cachedItems);
        }

        public ActionResult Clear(string token, string id)
        {
            CheckToken(token);

            CkanClient client = CkanHelper.GetClient();

            if (String.IsNullOrEmpty(id))
            {
                client.ClearCache();
            }
            else
            {
                client.ClearCache(id);
            }

            return RedirectToAction("Index", new { token = token });
        }

        /// <summary>
        /// Checks if the token provided is valid or if cache adminstration is enabled.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private void CheckToken(string token)
        {
            bool valid = false;
            string adminToken = SettingsHelper.GetCacheAdminToken();
            if (!String.IsNullOrEmpty(adminToken) && !String.IsNullOrEmpty(token))
            {
                if (adminToken == token)
                {
                    valid = true;
                }
            }

            if (!valid)
            {
                throw new SecurityException("Unauthorized attempt to administer the cache");
            }
        }

    }
}
