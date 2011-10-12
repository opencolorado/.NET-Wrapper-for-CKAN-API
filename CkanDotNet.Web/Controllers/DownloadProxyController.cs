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
    public class DownloadProxyController : Controller
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
            if (SettingsHelper.GetDownloadProxyEnabled())
            {
                StreamDownload(path);
            }
            else
            {
                //TODO: Handle this appropriately
                Response.End();
            }
        }

        /// <summary>
        /// Stream the resource to the client
        /// </summary>
        /// <param name="path"></param>
        private void StreamDownload(string path)
        {
            // Get the base proxy location
            Uri proxyLocation = SettingsHelper.GetDownloadProxyLocation();

            // Get the full uri to the resource
            Uri contentUri = new Uri(proxyLocation, path);

            long bytes = 0;

            // Get the HttpContext for the request going through this controller.
            HttpContextWrapper context = (HttpContextWrapper)HttpContext;

            // Process the download
            if (contentUri.IsFile || contentUri.IsUnc)
            {
                bytes = StreamFromFileResource(context, contentUri);
            }
            else if (contentUri.Scheme == "http" || contentUri.Scheme == "https")
            {
                bytes = StreamFromHttpResource(context, contentUri);
            }

            // Track the download in Google Analytics
            if (bytes > 0 && SettingsHelper.GetGoogleAnalyticsEnabled())
            {
                TrackDownloadEvent(path, context, bytes);
            }
        }

        /// <summary>
        /// Track the download event
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <param name="bytes"></param>
        private static void TrackDownloadEvent(string path, HttpContextWrapper context, long bytes)
        {
            ConfigurationManager.AppSettings["GoogleAnalyticsAccountCode"] = SettingsHelper.GetGoogleAnalyticsProfile();

            var code = GaDotNet.Common.Data.ConfigurationSettings.GoogleAccountCode;

            string referrer = "None";
            if (context.Request != null && context.Request.UrlReferrer != null)
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

        private long StreamFromFileResource(HttpContextWrapper context, Uri uri)
        {
            long bytes = 0;

            // Get the HttpResponse from this proxied request.
            HttpResponseWrapper response = (HttpResponseWrapper)context.Response;

            if (System.IO.File.Exists(uri.LocalPath))
            {
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
            }
            else
            {
                response.StatusCode = 404;
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

            try
            {
                using (WebResponse serverResponse = req.GetResponse())
                {
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
                }

            }
            catch (WebException ex)
            {
                int statusCode = 404;
                HttpWebResponse errorResponse = (HttpWebResponse)ex.Response;
                if (errorResponse != null)
                {
                    statusCode = (int)errorResponse.StatusCode;
                }
                
                response.StatusCode = statusCode;
            }

            return bytes;
        }
        
    }
}
