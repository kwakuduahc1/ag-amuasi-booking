import { Component, computed, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AddServiceDto } from '../../models/services.dto';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { ActivityProvider } from '../../../providers/ActivityProvider';

@Component({
  selector: 'app-services-add-component',
  imports: [
    CommonModule,
    FormsModule,
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
  protected data = inject<AddServiceDto | null>(MAT_DIALOG_DATA);
  private dialogRef = inject(MatDialogRef<ServicesAddComponent>);
  protected act = inject(ActivityProvider);

  form = new FormGroup({
    serviceName: new FormControl<string>(this.data?.serviceName ?? '', {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ]
    }),
    cost: new FormControl<number>({ value: this.data?.cost || Number.NaN, disabled: !!this.data }, {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.min(0.01),
      ]
    }),
    perPerson: new FormControl<boolean>(this.data?.perPerson ?? false, {
      nonNullable: true
    })
  });

  getText = computed(() => {
    if (this.act.activity().isProcessing)
      return { icon: 'hour_glass', text: 'Saving' }
    else if (this.data)
      return { icon: 'edit', text: 'Update' }
    else return { icon: 'add', text: 'Add' }
  })

  addService(form: Partial<AddServiceDto>) {
    form.cost = form?.cost || this.data?.cost || 0;
    this.dialogRef.close(form);
  }

  onCancel() {
    this.dialogRef.close();
  }
}
