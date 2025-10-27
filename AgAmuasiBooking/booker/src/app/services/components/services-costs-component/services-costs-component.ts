import { Component, inject, linkedSignal, model } from '@angular/core';
import { ServicesDto } from '../../models/services.dto';
import { SnackBarService } from '../../../providers/snackbar-service';
import { ServicesHttpService } from '../../services-http.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { pipe } from 'rxjs';

@Component({
  selector: 'app-services-costs-component',
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButton,
    MatIcon
  ],
  templateUrl: './services-costs-component.html',
  styleUrl: './services-costs-component.scss'
})
export class ServicesCostsComponent {
  protected service = inject<ServicesDto>(MAT_DIALOG_DATA);
  costs = linkedSignal(() => this.service.costs ?? []);
  private snack = inject(SnackBarService);
  private http = inject(ServicesHttpService);
  private diagRef = inject(MatDialogRef);
  form = new FormGroup({
    cost: new FormControl<number>(NaN, {
      nonNullable: true,
      validators: [Validators.required, Validators.min(10), Validators.max(10_000)]
    })
  });


  addCost(cost: Partial<{ cost: number }>) {
    this.http.addCost({
      id: this.service.servicesID,
      cost: cost.cost || 0
    })
      .subscribe(r => {
        this.snack.open('Cost added successfully');
        // this.service.cost = cost.cost || 0;
        this.costs.update(o => [{ serviceCostsID: r, servicesID: this.service.servicesID, cost: cost.cost || 0, dateSet: new Date() }, ...o]);
        this.form.reset();
      });
  }

  close() {
    this.diagRef.close(this.service);
  }
}
