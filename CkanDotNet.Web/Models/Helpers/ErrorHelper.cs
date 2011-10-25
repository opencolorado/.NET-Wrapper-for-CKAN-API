using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CkanDotNet.Web.Models.Helpers
{
    public static class ErrorHelper
    {
        /// <summary>
        /// Gets a default error presentation to display to the user.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ErrorPresentation GetDefaultErrorPresentation(Exception ex)
        {
            ErrorPresentation errorPresentation = new ErrorPresentation();
            errorPresentation.Exception = ex;

            // Set default messaging
            errorPresentation.Title = "Internal Error";
            errorPresentation.Description = "An internal error has occurred.";

            // If this is an HttpException provide standard HTTP error messages
            if (ex is HttpException)
            {
                HttpException httpEx = ex as HttpException;
                int statusCode = httpEx.GetHttpCode();

                // Set the status code
                errorPresentation.StatusCode = statusCode;

                // Populate the message
                switch (statusCode)
                {
                    case 404:
                        errorPresentation.Title = "Page Not Found";
                        errorPresentation.Description = "The page you are looking for could not be found";
                        break;
                }

            }

            // If the error is a custom descriptive http exception use those values
            if (ex is DescriptiveHttpException)
            {
                DescriptiveHttpException descEx = ex as DescriptiveHttpException;
                if (!String.IsNullOrEmpty(descEx.Message))
                {
                    errorPresentation.Title = descEx.Message;
                }

                if (!String.IsNullOrEmpty(descEx.Description))
                {
                    errorPresentation.Description = descEx.Description;
                }

            }


            return errorPresentation;
        }

        /// <summary>
        /// Display the error view
        /// </summary>
        /// <param name="errorPresentation"></param>
        public static void DisplayErrorView(ErrorPresentation errorPresentation)
        {

            // Return the error view
            ViewResult result = new ViewResult();
            result.ViewName = "Error";
            result.ViewData = new ViewDataDictionary(errorPresentation);

            // Set up the breadcrumbs for the view
            var breadCrumbs = new BreadCrumbs();

            breadCrumbs.Add(new BreadCrumb(
                "Home",
                "Index",
                "Home"));

            breadCrumbs.Add(new BreadCrumb(
                "Error"));

            result.ViewData.Add("BreadCrumbs", breadCrumbs);

            // Get the context for the error controller
            HttpContext context = HttpContext.Current;
            RequestContext requestContext = ((MvcHandler)context.CurrentHandler).RequestContext;
            //string controllerName = rc.RouteData.GetRequiredString("Error");
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = factory.CreateController(requestContext, "Error");
            ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

            // Execute the view results
            result.ExecuteResult(controllerContext);
            context.Server.ClearError();
        }
    }
}