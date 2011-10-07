using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CkanDotNet.Web.Models;

namespace CkanDotNet.Web.Controllers
{
    public class SuggestionController : Controller
    {
        //
        // GET: /Suggestion/

        public JsonResult Index(string prefix)
        {
            List<string> results = CkanHelper.GetPackageSearchSuggestions(prefix);
            return Json(results,"text/json",JsonRequestBehavior.AllowGet);
        }

    }
}
