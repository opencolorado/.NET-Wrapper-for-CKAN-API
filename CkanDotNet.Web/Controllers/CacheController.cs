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
    [HandleError]
    public class CacheController : Controller
    {
        //
        // GET: /Cache/
        [HandleError]
        public ActionResult Index(string key)
        {
            if (!IsValidKey(key))
            {
                throw new SecurityException("Unauthorized attempt to clear the cache");
            }
            return View();
        }

        [HandleError]
        public ActionResult Clear(string key)
        {
            if (IsValidKey(key))
            {
                CkanClient client = CkanHelper.GetClient();
                client.ClearCache();
            }
            else
            {
                throw new SecurityException("Unauthorized attempt to clear the cache");
            }
            return View();
        }

        /// <summary>
        /// Checks if the key provided is valid or if cache adminstration is enabled.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsValidKey(string key)
        {
            bool valid = false;
            string adminKey = SettingsHelper.GetCacheAdminKey();
            if (!String.IsNullOrEmpty(adminKey) && !String.IsNullOrEmpty(key))
            {
                if (adminKey == key)
                {
                    valid = true;
                }
            }
            return valid;
        }

    }
}
