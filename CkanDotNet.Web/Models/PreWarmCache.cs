using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CkanDotNet.Web.Models.Helpers;
using log4net;
using System.Reflection;

namespace CkanDotNet.Web.Models
{
    public class PreWarmCache : System.Web.Hosting.IProcessHostPreloadClient
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Provides support for auto-start.  See 
        /// http://weblogs.asp.net/scottgu/archive/2009/09/15/auto-start-asp-net-applications-vs-2010-and-net-4-0-series.aspx
        /// </summary>
        /// <param name="parameters"></param>
        public void Preload(string[] parameters)
        {
            // Configure log4net
            log4net.Config.XmlConfigurator.Configure();

            log.Debug("Preloading application.");

            // Start caching of auto-cached CKAN requests if we aren't in offline mode
            try
            {
                if (!SettingsHelper.GetOfflineEnabled())
                {
                    log.Debug("Preloading package count cache.");
                    if (SettingsHelper.GetPackageCountCacheBackgroundUpdate())
                    {
                        CkanHelper.GetPackageCount();
                    }

                    log.Debug("Preloading all packages cache.");
                    if (SettingsHelper.GetAllPackagesCacheBackgroundUpdate())
                    {
                        CkanHelper.GetAllPackages();
                    }

                    log.Debug("Preloading all licenses cache.");
                    if (SettingsHelper.GetAllLicensesCacheBackgroundUpdate())
                    {
                        CkanHelper.GetLicenses();
                    }
                }
                else
                {
                    log.Debug("Application is offline.  Cache will not be preloaded.");
                }
            }
            catch (Exception ex)
            {
                // If any errors occur while trying to start caching, log this and clear the error.
                // Any new page request will re-attempt to start the caching process
                log.Error("Unable to starting background caching on preload", ex);
            }
        }  
    }
}