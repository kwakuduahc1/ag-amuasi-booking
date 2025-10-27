import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent)
    },
    {
        path: 'home',
        redirectTo: '',
    },
    {
        path: 'auth',
        loadChildren: () => import('./auth/auth.routes').then(m => m.AUTH_ROUTES)
    },
    {
        path: 'services',
        loadChildren: () => import('./services/services.routes').then(m => m.SERVICES_ROUTES)
    },
    {
        path: 'my-bookings',
        loadChildren: () => import('./my-bookings/my-bookings.routes').then(m => m.MY_BOOKING_ROUTES)
    }
];
