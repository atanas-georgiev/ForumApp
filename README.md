# ForumApp
Telerik Task Assignment

####Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/8dskbn908e27vevx?svg=true)](https://ci.appveyor.com/project/atanas-georgiev/forumapp)

####Demo link
[http://forum.atanas.it](http://forum.atanas.it) - hosted with IIS 10 on Windows Server

##Description
ForumApp is a simple web-based forum application supporting following functionality:
- View predefined forums
- Creating new Posts in each forum (no registration required)
- Creating new Comments in each post (no registration required)

##Created with
- ASP.NET MVC 5
- .NET 4.6
- Entity Framework 6 with SQL server as database **(requirement 2)**
- Razor view engine
- Bootstrap

##Implementation details
* 3 views implemented **(requirement 1)**. Most recent data at the bottom, check by Unit Test GetForumPostsShouldBeSortedByDateInAscendingOrder()
  * Home - for list of forums
  *  Forum - for list of posts
  * Post - for list of comments
* Implemented paging of forums, posts and comments **(requirement 1.2)**
* Used repository pattern to access database (ForumApp.Data)
* Used dependency injection for Data Repositories, Services and Controllers(Autofac)
* Used automatic mapping between DbObjects and ViewModels(Automapper)
* Implemented database models in ForumApp.Data.Models **(requirement 2.2)**
* Used services for main functionality in Forums, Posts and Comments (ForumApp.Services)
* Separated caching functionality in a service (ForumApp.Services.CacheService). 15 minutes cache used for posts and comments **(requirement 1.1)**. Verified by Unit Tests HomeIndexWithParamsShouldPointToCorrectRoute() and ForumIndexWithParamsShouldPointToCorrectRoute()
* Implemened adding new posts and comments using AJAX in the list page **(requirement 3)**. Input forms allow adding multiline text and HTML symbols (XSS secured by HTML sanitizer)
* Unit tests **(requirement 4)**
  * Services unit tests (100% code coverage)
  * MVC controllers unit tests (100% code coverage, used FluentMVCTesting Nuget lib)
  * Routing unit tests (100% route coverage, used MvcRouteTester Nuget lib)
* StyleCop coding style rules respected (0 warnings in complete code)
* Used CI (AppVeyor + WEB Deploy to IIS)


