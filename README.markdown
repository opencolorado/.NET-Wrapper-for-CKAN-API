## Welcome
This project provides an [ASP.NET MVC 3] (http://www.asp.net/mvc/mvc3) front-end for a 
[CKAN] (http://wiki.ckan.net/Main_Page) repository.  This project provides
a fully functioning web-based data catalog that is specific to a group in a shared CKAN respository.

### About
This project was created in partnership with [Open Colorado] (http://www.opencolorado.org) and 
Colorado Smart Communities.  

This .NET implementation was created for communities that want to leverage the common CKAN 
repository (http://colorado.ckan.net) while hosting a local data catalog front-end on their organization's
Windows/.NET platform.  For organizations that prefer, there is also 
a [PHP front-end] (https://github.com/opencolorado/PHP-Wrapper-for-CKAN-API)  available.

### Features
* Customizable home page:
 * Display featured packages (packages tagged with 'featured')
 * Show recently updated packages
 * Show popular tags
 * Show total number of packages that are available
 * Customizable welcome information, usage information and about information
* Search for data packages by query or by tag
* Paginated search results
* View package details including:
 * 
* Designed to present a data catalog based on a CKAN group
* Customizable/configurable data catalog front-end
* Home page provides:
 * Welcome message
 * Search
 * Featured packages (packages tagged with 'featured')
 * Recently updated packages
 * Top tags
* Search screen provides:
 * Pagination
 * Tag filtering
 * Breadcrumbs for search parameters
* Package screen provides:
 * Description
 * Resource list
 * Package details
 * Rate this package
 * License information

The catalog is fully configurable and it is designed to be easy to integrate with the look 
feel or an existing site.

## Getting Started

### System Requirements
* Windows Server 2003 or greater
* Microsoft Internet Information Services 6 or greater
* .NET Framework 4
* ASP.NET MVC 3

## Development Tools
* Microsoft Visual Web Developer Express 2010
* xUnit 1.8

## Dependencies
* Client:
 * jQuery 1.6.2
 * jQuery plugin - Star Rating widget (http://plugins.jquery.com/project/Star_Rating_widget)
 * jQuery plugin - tipTip (http://code.drewwilson.com/entry/tiptip-jquery-plugin)
 * jQuery plugin - watermark (http://jquery-watermark.googlecode.com/)
* Server
 * log4net (http://logging.apache.org/log4net/)
 * RestSharp (http://restsharp.org/)

## Release Notes
See https://github.com/opencolorado/.NET-Wrapper-for-CKAN-API/wiki/Release-Notes

## CkanDotNet.Web
Provides a ASP.NET MVC 3 front-end for the [CKAN API Version 2][1] that is intended to be used for providing a fully functioning data catalog.  Uses the CkanDotNet.Api library (see below).



## CkanDotNet.Api
Provides a .NET wrapper for the [CKAN API Version 2][1].

### Features
* Provides the following support for the Model API
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

* Provides the following support for the Search API
 * Package Search

[1]: http://docs.ckan.org/en/latest/api.html#api-details-versions-1-2