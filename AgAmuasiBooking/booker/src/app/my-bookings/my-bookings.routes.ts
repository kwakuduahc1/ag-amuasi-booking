import { inject } from "@angular/core";
import { Routes } from "@angular/router";
import { MyBookingsService } from "./my-bookings.service";
import { ServicesHttpService } from "../services/services-http.service";

export const MY_BOOKING_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/my-bookings-component/my-bookings-component').then(m => m.MyBookingsComponent),
        resolve: {
            bookings: () => inject(MyBookingsService).list(5),
            services: () => inject(ServicesHttpService).getServices()
        }
    }
];
