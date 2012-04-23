See https://github.com/opencolorado/.NET-Wrapper-for-CKAN-API for additional information.

## Version 1.9 (4/22/2012)
* Removed created date from package page, now shows last updated date

## Version 1.8 (3/19/2012)
* Fixed bug with dataset timestamps not being converted from UTC
* Resource links not open in a new window

## Version 1.7 (3/13/2012)
* jQuery version is now a config setting
* Improved dynamic resizing when in an iframe (now supports resizing after Disqus comments loaded)
* Fixed bug with suggestions (search autocomplete) when not installed in root of the web server
* Fixed layout of license information when sidebar is narrow
* Added support for ASP.NET 4.0/IIS 7.5 [auto-start] (http://weblogs.asp.net/scottgu/archive/2009/09/15/auto-start-asp-net-applications-vs-2010-and-net-4-0-series.aspx) to precache data from the CKAN repository

## Version 1.6 (3/11/2012)
* Improvements and bug fixes
* Added support for diplaying in an iframe
* Optimized layout to support more advanced theme control
* Fixes for IE7/IE8

## Version 1.5 (12/21/2011)
* Added support for multiple download proxy locations 
* Added true tag cloud support

## Version 1.4 (10/25/2011)
* Enhanced support for the download proxy
* Improved error handling and messaging to the end user
* General code improvements and optimizations

## Version 1.3 (10/12/2011)
* Added autocomplete for search box
 * Suggestions are provided for package names and tags
* Added experimental support for a download proxy that allows download tracking even when a resource is downloaded through an external catalog link (ex. OpenColorado).  
 * If Google Analytics are enabled, individual resource downloads are tracked including the total bytes for each download.

## Version 1.2 (10/5/2011)
* Added support for [AddThis] (http://www.addthis.com) social medial sharing (optional)
 * Includes support for 'print this page' and sending the page by email
* Added support for [UserVoice] (http://www.uservoice.com) for suggesting new datasets (optional)
* Miscellaneous code improvements and bug fixes

## Version 1.1 (9/21/2011)
* Implemented new features for search engine optimization
 * Added auto-generated sitemap (sitemap.xml)
 * Added configurable meta tags for homepage (keywords and description)
 * Added automated meta tags for packages (keywords and description)
* Added support for [Google Analytics] (http://www.google.com/analytics) including event tracking for resource downloads, search etc. (optional)
* Added support for [DISQUS] (http://www.disqus.com) commenting on packages (optional)

## Version 1.0 (9/5/2011)
* Implemented cache admin page for viewing the cache and clearing items from the cache on demand
* Added improved error handling for failed CKAN requests including a configurable timeout
* Added support for taking the catalog offline with a custom offline message.
* Added detailed documentation for all parameters in web.config and reorganized the settings
* Miscellaneous code improvements and bug fixes

## Version 0.4 (8/8/2011)
* Added tag route for tidy tag URL (ex. /tag/<tag>)
* Implemented background caching support for main page items that are costly to update.
* Added support for CKAN markdown formatting in package descriptions
* Implemented dynamic handling of CKAN package 'extra' properties including configurable labels
* Documented project dependencies/third party libraries
* Package resources section is not displayed if there are no resources

## Version 0.3 (7/31/2011)
* Added RSS feed for package revisions
* Implemented configurable resource actions including multiple actions for some resources.
* Implemented caching pattern for CKAN API wrapper that allows all request types to be cached for performance.
* Fixed IE7 & IE8 CSS issues.
* General user interface improvements to the default theme.
* Bug fixes.

## Version 0.2 (7/24/2011)
* Handles inconsistent date formats in CKAN repositories.
* Completed full unit testing of CKAN API.
* Moved all parameters to config file.
* Added initial caching for CKAN requests.
* Added logging (log4net)

## Version 0.1 (7/17/2011)
* Initial published prototype.
