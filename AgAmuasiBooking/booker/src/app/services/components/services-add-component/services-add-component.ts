import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { AddServiceDto } from '../../models/services.dto';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-services-add-component',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIcon
  ],
  templateUrl: './services-add-component.html',
  styleUrl: './services-add-component.scss'
})
export class ServicesAddComponent {
  private data = inject<AddServiceDto | null>(MAT_DIALOG_DATA);
  private dialogRef = inject(MatDialogRef<ServicesAddComponent>);

  isSubmitting = false;

  form = new FormGroup({
    serviceName: new FormControl<string>('', {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ]
    }),
    cost: new FormControl<number>(0, {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.min(0.01),
      ]
    }),
    perPerson: new FormControl<boolean>(false, {
      nonNullable: true
    })
  });

  onSubmit() {
    if (this.form.valid) {
      this.isSubmitting = true;
      const formData: AddServiceDto = {
        serviceName: this.form.value.serviceName!,
        cost: this.form.value.cost!,
        perPerson: this.form.value.perPerson!
      };

      // TODO: Implement service creation logic
      console.log('Service data:', formData);

      // Close dialog and return the data
      this.dialogRef.close(formData);
    } else {
      // Mark all fields as touched to show validation errors
      this.form.markAllAsTouched();
    }
  }

  onCancel() {
    this.dialogRef.close();
  }

  // Dynamic validation getters for template use
  get serviceNameMaxLength(): number {
    const maxLengthValidator = this.form.get('serviceName')?.hasError('maxlength');
    return 100; // Default fallback, could be extracted from validator if needed
  }

  get costMinValue(): number {
    return 0.01; // Could be extracted from Validators.min if needed
  }

  get costMaxValue(): number {
    return 999999.99; // Could be extracted from validator if added
  }
}
