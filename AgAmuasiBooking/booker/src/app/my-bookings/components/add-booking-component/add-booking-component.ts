import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ServicesDto } from '../../../services/models/services.dto';

@Component({
  selector: 'app-add-booking-component',
  imports: [],
  templateUrl: './add-booking-component.html',
  styleUrl: './add-booking-component.scss'
})
export class AddBookingComponent {
  services = inject<ServicesDto[]>
  form = new FormGroup({
    title: new FormControl<string>('', {
      nonNullable: true,
      validators: [
        Validators.minLength(3),
        Validators.maxLength(50)
      ]
    }),
    purpose: new FormControl<string>('', {
      nonNullable: true,
      validators: [
        Validators.maxLength(200),
        Validators.minLength(5)
      ]
    }),
    guests: new FormControl<number>(1, {
      nonNullable: true,
      validators: [
        Validators.min(1),
        Validators.max(20),
        Validators.required
      ]
    })
  });


}
