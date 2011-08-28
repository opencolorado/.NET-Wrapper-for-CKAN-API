#DRAFT

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

## Features
* Customizable home page:
 * Display featured packages (packages tagged with 'featured')
 * Show recently updated packages
 * Show popular tags
 * Show total number of packages that are available
 * Customizable welcome information, usage information and about information
* Search for data packages by query or by tag:
 * Paginated search results
 * Filter results by tag
* View package details including:
 * Description
 * Resources with customizable resource actions (ex. View KML in Google Maps)
 * Package rating (and 'rate this package')
 * License details for each package
* Themeable/skinnable:
 * The catalog supports themes (just copy an existing theme and customize from there)
 * Theming uses CSS and HTML
 * Designed to be integrated into an organizations web site with no code changes required
* Highly configurable

## Getting Started

### System Requirements
* Windows Server 2003 or greater
* Microsoft Internet Information Services 6 or greater
* .NET Framework 4
* ASP.NET MVC 3

## Release Notes
See https://github.com/opencolorado/.NET-Wrapper-for-CKAN-API/wiki/Release-Notes

## Want to Contribute?

### Development Tools
* Microsoft Visual Web Developer Express 2010
* xUnit 1.8

### Dependencies
* Client:
 * jQuery 1.6.2
 * jQuery plugin - Star Rating widget (http://plugins.jquery.com/project/Star_Rating_widget)
 * jQuery plugin - tipTip (http://code.drewwilson.com/entry/tiptip-jquery-plugin)
 * jQuery plugin - watermark (http://jquery-watermark.googlecode.com/)
* Server
 * log4net (http://logging.apache.org/log4net/)
 * RestSharp (http://restsharp.org/)


# CkanDotNet.Api
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