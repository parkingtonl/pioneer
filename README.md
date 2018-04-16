# pioneer
Aurelia Navigation Hierarchy From Database (dynamic)

This program demonstrates how to retrieve data from an SQL Server database to create a dynamic menu in a NET Core Aurelia application

In essence it retrieves the route data from the database and writes to the TypeScript file routes.ts which is then compiles the routes as static references which are then used in the navigation. This is dynamic and allows real time changes to the navigation menu.

Requires an SQL Server Express database using LocalDB named AureliaNavigation (using Windows Authentication) and NavigationRoute table created using the AutrliaNavigation-NavigationRoute.sql script to work correctly. A Scheduler menu item should appear in the AureliaDbMenu navigation and behave correctly.

Note: The server name in appsettings.json (Server=LANCE-LENOVO;Database=AureliaNavigation;Trusted_Connection=True;) will need to be changed to use the name of your local machine
