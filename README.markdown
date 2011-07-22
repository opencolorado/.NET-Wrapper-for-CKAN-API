# .NET Wrapper for CKAN API

## Release Notes
See https://github.com/opencolorado/.NET-Wrapper-for-CKAN-API/wiki/Release-Notes

## CkanDotNet.Web
Provides a ASP.NET MVC 3 front-end for the [CKAN API Version 2][1] that is intended to be used for providing a fully functioning data catalog.  Uses the CkanDotNet.Api library (see below).

### Features
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

[1]: http://packages.python.org/ckan/api/version2.html