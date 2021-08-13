# ConsoleApps

This is a practice project to work on an Entity Framework Core application without worrying about ASP.NET componenents.
The applications stores Book and Author objects in a database.

Currently running on:

* EF Core 5.1

## What can you do?
Current features include:
* List books.
* Search books by title.
* Search books by number of pages within +-50
* Add a book and author.

## TODO:
* List authors. Make sure if an author already exists when added with a book it isn't recreated.
* Reorganize menus. Put the right functions in the right scopes, clean up, make more dynamic.
* Searching books by everything else.
* Searching by number of pages improvements: setting range, setting range above and below.
* Searching by author.
* Making changes to existing objects.