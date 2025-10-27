import { inject, Injectable, input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { AddBookingDto } from './models/add-reservation';

@Injectable({ providedIn: 'root' })
export class MyBookingsService {
    private http = inject(HttpClient);
    protected services = input.required

    list(num: number): Observable<any[]> {
        return this.http.get<any[]>(environment.AppUrl + `MyBookings/${num}`);
    }

    create(booking: AddBookingDto): Observable<any> {
        return this.http.post<any>(environment.AppUrl + 'MyBookings', booking);
    }
}
