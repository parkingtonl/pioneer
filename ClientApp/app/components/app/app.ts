import { Aurelia, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';
import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import './routes'; // this typescript file includes the PLATFORM.moduleName('../scheduler/scheduler') statements for each route

@inject(HttpClient)
export class App {
    public navroutes: NavigationRoute[];
    constructor(http: HttpClient) {
        // Test database connection ok
        http.fetch('api/SampleData/TestDbConn')
            .then(result => result.json() as Promise<NavigationRoute[]>)
            .then(data => {
                var routes = data;
                if (routes.length == 1) {
                    alert('Sql Server database not set up or configured correctly');
                }

            });

        // Read the navigation from database
        http.fetch('api/SampleData/DbRoutes')
            .then(result => result.json() as Promise<NavigationRoute[]>)
            .then(data => {
                this.navroutes = data;
                var subroutes: NavigationRoute[] = [];
                // Add each route (including sub route(s) here
                for (var i = 0; i < data.length; i++) {
                    var navroute: NavigationRoute = <NavigationRoute>data[i];
                    if (navroute.parentId == null) {
                        this.router.addRoute({
                            route: navroute.route,
                            name: navroute.name,
                            settings: { icon: navroute.icon },
                            moduleId: PLATFORM.moduleName(navroute.moduleId.toString()), // the static references to the routes are defined in app/routes.ts
                            nav: navroute.nav,
                            title: navroute.title
                        })
                        this.router.refreshNavigation();
                    }
                }
            });

        http.fetch('api/SampleData/WriteRoutesToFile')
            .then(result => result.json() as Promise<NavigationRoute[]>)
            .then(data => {


            });
    }

    router: Router;

    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'Demo';

        config.map([{
            route: ['', 'home'],
            name: 'home',
            settings: { icon: 'home' },
            moduleId: PLATFORM.moduleName('../home/home'),
            nav: true,
            title: 'Home' // The default home
        }]);

        this.router = router;
    }
}

class NavigationRoute {
    id: number;
    parentId: any;
    route: string;
    name: string;
    icon: string;
    moduleId: string;
    nav: boolean;
    title: string;
}

