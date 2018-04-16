import { Aurelia, PLATFORM } from 'aurelia-framework'; 
import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';

@inject(HttpClient)
export class DbRoutes {
    public navroutes: NavigationRoute[];
    public routes: string[];
    constructor(http: HttpClient) { 
        PLATFORM.moduleName('../scheduler/scheduler');
        PLATFORM.moduleName('../newcustomer/newcustomer');
        PLATFORM.moduleName('../searchcustomers/searchcustomers');
        PLATFORM.moduleName('../counter/counter');
        PLATFORM.moduleName('../fetchdata/fetchdata');
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
};
