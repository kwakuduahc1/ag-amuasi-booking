import { Component, inject, model } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatDivider } from '@angular/material/divider';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-services-list-component',
  imports: [],
  templateUrl: './services-list-component.html',
  styleUrl: './services-list-component.scss'
})
export class ServicesListComponent {
  protected services = model();
  private diag = inject(MatDialog);
  private snack = inject(MatSnackBar);
}
