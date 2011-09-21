## Welcome
This project provides an [ASP.NET MVC 3] (http://www.asp.net/mvc/mvc3) front-end for a 
[CKAN] (http://wiki.ckan.net/Main_Page) repository.  This project provides
a fully functioning web-based data catalog based on a group in a shared CKAN respository.

In addition, this project provides a C# wrapper for the [CKAN API Version 2][1] for developers
that want to develop .NET applications against any CKAN repository.

### About
This project was created in partnership with [Open Colorado] (http://www.opencolorado.org) and 
Colorado Smart Communities.  

This .NET implementation was created for communities that want to leverage the Colorado CKAN 
repository (http://colorado.ckan.net) while hosting a local data catalog front-end on their organization's
Windows/.NET platform.  For organizations that prefer, there is also 
a [PHP front-end] (https://github.com/opencolorado/PHP-Wrapper-for-CKAN-API) available.

There is no specific implemention in this project related to colorado.ckan.net so this can be
used with any CKAN instance.

### Current Version

Version 1.1, released 9/21/2011.  For details see the [Release Notes]
(https://github.com/opencolorado/.NET-Wrapper-for-CKAN-API/blob/master/ReleaseNotes.markdown)

## Features
* Customizable home page
 * Show featured packages (packages tagged with 'featured')
 * Show recently updated packages
 * Show popular tags
 * Show total number of packages that are available
 * Customizable text content and layout
* Search for data packages by query or by tag
 * Paginated search results
 * Filter results by tag
 * Optionally show package ratings with search results
* View package details
 * Date posted and date last updated
 * Package description with support for CKAN Markdown formatting
 * Resources with customizable resource actions (ex. View KML in Google Maps)
 * Additional package details
 * Package rating (and 'rate this package')
 * License details
 * RSS feed for package revisions
* Themeable
 * Supports custom themes with CSS/HTML (just copy an existing theme and customize from there)
 * Designed to be integrated into an organizations web site with no code changes required
* Optimized for search engines
 * Provides an auto-generated site map (sitemap.xml) for submitting to search engines
 * Meta tag support on home page and package pages
* Supports [Google Analytics] (http://www.google.com/analytics) including event tracking for resource downloads, search terms etc. (optional)
* Supports [DISQUS] (http://www.disqus.com) commenting on packages. (optional)
* Configurable CKAN request caching for performance
 * Supports automatic background caching
 * A cache admin interface is provided to view cache contents and clear items from the cache if CKAN changes need to be reflected immediately
* Maintenance mode
 * Catalog can be taken offline with a custom notice if the CKAN repository needs maintenance
* Uses clean RESTful URLs
* Provides breadcrumbs
* Configurable logging for system monitoring and troubleshooting

## Getting Started

### System Requirements
* Windows Server 2003 or greater
* Microsoft Internet Information Services 6 or greater


### Installation

#### Install and Configure Prerequisites
* .NET Framework 4
* ASP.NET MVC 3

#### Install Web Application
* Download the latest stable build
* Unzip and install the web application on your web server
* Configure the settings in web.config
* Access the URL of the application

## Want to Contribute?

Feel free to fork this repo and contribute your ideas.

This project was created using a mix of free tools and open source libraries.  You will need the following
to compile the project from source and run the unit tests:

* [Microsoft Visual Web Developer Express 2010] (http://www.microsoft.com/visualstudio/en-us/products/2010-editions/visual-web-developer-express) 
* [xUnit] (http://xunit.codeplex.com)

### Frameworks Used
* Client:
 * jQuery 1.6.2
 * jQuery plugin - [Star Rating widget] (http://plugins.jquery.com/project/Star_Rating_widget)
 * jQuery plugin - [tipTip] (http://code.drewwilson.com/entry/tiptip-jquery-plugin)
 * jQuery plugin - [watermark] (http://jquery-watermark.googlecode.com/)
* Server
 * [log4net] (http://logging.apache.org/log4net/)
 * [RestSharp] (http://restsharp.org/)

## C# CKAN API Wrapper

As mentioned above, this project includes a .NET wrapper for the CKAN REST API.  This wrapper was created for the
needs of this project but it can be used stand-alone if you just need a .NET library to communicate with CKAN.

The following CKAN features are supported at this time:

### Model API
* Package Register
* Package Entity
* Group Register
* Group Entity
* Tag Register
* Tag Entity
* Package’s Revisions Entity
* Revision Register
* Revision Entity
* License List

### Search API
* Package Search

[1]: http://docs.ckan.org/en/latest/api.html#api-details-versions-1-2