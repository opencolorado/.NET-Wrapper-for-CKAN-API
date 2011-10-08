using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using GaDotNet.Common;
using GaDotNet.Common.Data;
using GaDotNet.Common.Helpers;
using CkanDotNet.Web.Models.Helpers;
using System.Configuration;

namespace CkanDotNet.Web.Controllers
{
    public class ContentProxyController : Controller
    {
        /// <summary>
        /// Provides a reverse proxy to content that is provided via the
        /// catalog.  Provided to allow capturing of analytics
        /// for downloads that originate from the master catalog website
        /// while also simplifying the setup by allowing download links to go
        /// through the catalog web application.
        /// GET: /content/{path}
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void Index(string path)
        {
            // Get the HttpContext for the request going through this controller.
            HttpContextWrapper context = (HttpContextWrapper)HttpContext;

            Uri uri = new Uri("http://maps.arvada.org/opendata/");

            //uri = new Uri("file://c:/temp/");

            uri = new Uri(@"\\localhost\c$\temp\");
            // Get the full uri to the resource
            Uri contentUri = new Uri(uri,path);

            long bytes = 0;

            // Handle the download accordingly
            if (uri.IsFile || uri.IsUnc)
            {
                bytes = StreamFromFileResource(context, contentUri);
            }
            else if (uri.Scheme == "http" || uri.Scheme == "https")
            {
                bytes =StreamFromHttpResource(context, contentUri);
            }

            // Track the download in Google Analytics
            if (SettingsHelper.GetGoogleAnalyticsEnabled())
            {
                ConfigurationManager.AppSettings["GoogleAnalyticsAccountCode"] = SettingsHelper.GetGoogleAnalyticsProfile();

                var code = GaDotNet.Common.Data.ConfigurationSettings.GoogleAccountCode;

                string referrer = "None";
                if (context.Request.UrlReferrer != null)
                {
                    referrer = context.Request.UrlReferrer.Host;
                }

                int kiloBytes = (int)(bytes / 1024);

                GoogleEvent googleEvent = new GoogleEvent("http://localhost.local",
                    "Download",
                    referrer,
                    path,
                    kiloBytes);

                var app = (HttpApplication)context.GetService(typeof(HttpApplication));

                TrackingRequest request =
                    new RequestFactory().BuildRequest(googleEvent, app.Context);

                GoogleTracking.FireTrackingEvent(request);
            }

        }

        private long StreamFromFileResource(HttpContextWrapper context, Uri uri)
        {
            long bytes = 0;

            // Get the HttpResponse from this proxied request.
            HttpResponseWrapper response = (HttpResponseWrapper)context.Response;

            response.ContentType = MimeType(uri.LocalPath);

            // Transfer the stream to the response stream
            using (FileStream stream = new FileStream(uri.LocalPath, FileMode.Open))
            {
                using (BinaryWriter bw = new BinaryWriter(response.OutputStream))
                {
                    byte[] buffer = new byte[1];
                    while (stream.Read(buffer, 0, 1) > 0)
                    {
                        bytes++;
                        bw.Write(buffer);
                    }
                }
                response.End();

            }
            return bytes;
        }

        private string MimeType(string Filename)
        {
            string mime = "application/octetstream";
            string ext = System.IO.Path.GetExtension(Filename).ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        } 

        private long StreamFromHttpResource(HttpContextWrapper context, Uri uri)
        {
            long bytes = 0;

            // Get the HttpResponse from this proxied request.
            HttpResponseWrapper response = (HttpResponseWrapper)context.Response;

            // Create the request for the resource
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.Method = context.Request.HttpMethod;
            req.ServicePoint.Expect100Continue = false;
            req.Timeout = 20000;

            WebResponse serverResponse = req.GetResponse();

            // Set the return content type
            response.ContentType = serverResponse.ContentType;

            // Transfer the stream to the response stream
            using (Stream responseStream = serverResponse.GetResponseStream())
            {
                using (BinaryWriter bw = new BinaryWriter(response.OutputStream))
                {
                    byte[] buffer = new byte[1];
                    while (responseStream.Read(buffer, 0, 1) > 0)
                    {
                        bytes++;
                        bw.Write(buffer);
                    }
                }
                response.End();
            }
            return bytes;
        }
        
    }
}
