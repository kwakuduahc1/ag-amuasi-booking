import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { ServicesDto } from '../../../services/models/services.dto';
import { CloseButtonComponent } from "../../../components/close-button-component/close-button-component";
import { CommonModule } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { AddBookingDto } from '../../models/add-reservation';
import { MatError, MatFormField, MatInput, MatLabel } from '@angular/material/input';

@Component({
  selector: 'app-add-booking-component',
  imports: [
    CloseButtonComponent,
    ReactiveFormsModule,
    CommonModule,
    MatButton,
    MatInput,
    MatFormField,
    MatLabel,
    MatError
  ],
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
        Validators.maxLength(50),
        Validators.required
      ]
    }),
    purpose: new FormControl<string>('', {
      nonNullable: true,
      validators: [
        Validators.maxLength(200),
        Validators.minLength(5),
        Validators.required
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

  addReservation(form: Partial<AddBookingDto>) {
    console.log(form);
  }

}
