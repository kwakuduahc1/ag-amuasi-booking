import { inject } from "@angular/core";
import { Routes } from "@angular/router"
import { ServicesHttp } from "./services-http";

export const SERVICES_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/services-list-component/services-list-component')
            .then(m => m.ServicesListComponent),
        resolve: {
            services: () => inject(ServicesHttp).getServices()
        }
    }
];
