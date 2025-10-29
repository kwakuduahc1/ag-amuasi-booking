import { Component, inject, input } from '@angular/core';
import { ServicesDto } from '../../../services/models/services.dto';
import { UserBookingDto } from '../../models/add-reservation';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddBookingComponent } from '../add-booking-component/add-booking-component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-my-bookings-component',
  imports: [],
  templateUrl: './my-bookings-component.html',
  styleUrl: './my-bookings-component.scss'
})
export class MyBookingsComponent {
  services = input<ServicesDto[]>();
  bookings = input.required<UserBookingDto[]>();
  private snack = inject(MatSnackBar);
  private diag = inject(MatDialog);

  addBooking() {
    this.diag.open<AddBookingComponent, {}, any>(AddBookingComponent, {
      width: '600px',
      height: '800px'
    }).afterClosed()
      .subscribe();
  }
}
