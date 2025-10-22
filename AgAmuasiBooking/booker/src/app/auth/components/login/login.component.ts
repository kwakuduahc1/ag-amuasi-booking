import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { IUsers } from '../../../models/IUsers';
import { StatusProvider } from '../../../providers/StatusProvider';
import { MatIconButton } from '@angular/material/button';
import { ActivityProvider } from '../../../providers/ActivityProvider';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatIcon,
    MatIconButton,
    MatInputModule
  ]
})
export class LoginComponent {
  route = inject(Router);
  form: FormGroup;
  status = inject(StatusProvider);
  act = inject(ActivityProvider)
  private user? = JSON.parse(localStorage.getItem('user') as string) as IUsers;

  constructor() {
    this.form = new FormGroup({
      email: new FormControl(this.user?.email ?? "", Validators.compose([Validators.required, Validators.minLength(5), Validators.maxLength(20)])),
      password: new FormControl(this.user?.password ?? "", Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(15), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/)]))
    });
  }

  login(login: { email: string; password: string }): void {
    this.status.login(login)
      .subscribe(() => this.route.navigate(['/']));
  }
}

