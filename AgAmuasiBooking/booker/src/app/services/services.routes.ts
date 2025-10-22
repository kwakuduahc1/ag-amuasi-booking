import { Routes } from "@angular/router"

export const SERVICES_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/services-list-component/services-list-component')
            .then(m => m.ServicesListComponent)
    }
];
