import { Component, inject, model, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatIcon } from '@angular/material/icon';
import { ActivityProvider } from '../../../providers/ActivityProvider';
import { StatusProvider } from '../../../providers/StatusProvider';
import { CommonModule } from '@angular/common';
import { ServicesDto, AddServiceDto } from '../../models/services.dto';
import { ServicesAddComponent } from '../services-add-component/services-add-component';
import { MatAnchor } from "@angular/material/button";

@Component({
  selector: 'app-services-list-component',
  imports: [
    CommonModule,
    MatIcon,
    MatAnchor
  ],
  templateUrl: './services-list-component.html',
  styleUrl: './services-list-component.scss'
})
export class ServicesListComponent {
  protected services = model.required<ServicesDto[]>();
  private diag = inject(MatDialog);
  private snack = inject(MatSnackBar);
  private act = inject(ActivityProvider);
  service = signal<AddServiceDto | null>(null);
  status = inject(StatusProvider);

  addService() {
    console.log('Adding service - button clicked');

    const dialogRef = this.diag.open(ServicesAddComponent, {
      data: this.service(),
      width: '500px',
      disableClose: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Service added:', result);
        this.snack.open('Service added successfully', 'Close', {
          duration: 3000
        });
        // TODO: Add service to the list or refresh data
      }
    });
  }
}
