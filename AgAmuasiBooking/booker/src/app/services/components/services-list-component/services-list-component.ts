import { Component, inject, model } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivityProvider } from '../../../providers/ActivityProvider';
import { StatusProvider } from '../../../providers/StatusProvider';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-services-list-component',
  imports: [
    CommonModule
  ],
  templateUrl: './services-list-component.html',
  styleUrl: './services-list-component.scss'
})
export class ServicesListComponent {
  protected services = model();
  private diag = inject(MatDialog);
  private snack = inject(MatSnackBar);
  private act = inject(ActivityProvider);
  status = inject(StatusProvider);
}
