using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace CkanDotNet.Web.Models
{
    public class PartialRequest
    {
        public RouteValueDictionary RouteValues { get; private set; }

        public PartialRequest(object routeValues)
        {
            RouteValues = new RouteValueDictionary(routeValues);
        }

        public void Invoke(ControllerContext context)
        {
            RouteData rd = new RouteData(context.RouteData.Route, context.RouteData.RouteHandler);
            foreach (var pair in RouteValues)
                rd.Values.Add(pair.Key, pair.Value);
            IHttpHandler handler = new MvcHandler(new RequestContext(context.HttpContext, rd));
            handler.ProcessRequest(System.Web.HttpContext.Current);
        }
    }

    public static class PartialRequestsExtensions
    {
        public static void RenderPartialRequest(this HtmlHelper html, string viewDataKey)
        {
            PartialRequest partial = html.ViewContext.ViewData.Eval(viewDataKey) as PartialRequest;
            if (partial != null)
                partial.Invoke(html.ViewContext);
        }
    }
}