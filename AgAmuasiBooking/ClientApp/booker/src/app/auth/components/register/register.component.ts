import { Component, inject, input } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

import { MatSnackBar } from "@angular/material/snack-bar";
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LoginHttpService } from '../../../http/login-http-service';
import { ActivityProvider } from '../../../providers/ActivityProvider';
import { StatusProvider } from '../../../providers/StatusProvider';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { environment } from '../../../environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationComponent } from '../../../components/confirmation/confirmation.component';
import { filter, map, switchMap } from 'rxjs';
import { IUsers, RegisterVm, UsersDto } from '../../../models/IUsers';
import { httpResource } from '@angular/common/http';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatIcon
  ]
})
export class RegisterComponent {
  private snack = inject(MatSnackBar);
  private diag = inject(MatDialog);
  act = inject(ActivityProvider);
  http = inject(LoginHttpService);
  status = inject(StatusProvider);
  roles = input<string[]>();
  users = httpResource<UsersDto[]>(() =>
    environment.AppUrl + 'Auth/Users', {
    defaultValue: []
  });

  form = new FormGroup({
    fullName: new FormControl<string>('', [Validators.required, Validators.minLength(6), Validators.maxLength(50)]),
    userName: new FormControl<string>('', [Validators.required, Validators.minLength(6), Validators.maxLength(30)]),
    password: new FormControl<string>('',
      {
        validators: [Validators.required, Validators.minLength(6), Validators.maxLength(15), passwordComplexityValidator()]
      }),
    confirmPassword: new FormControl<string>('', {
      validators: [Validators.required, Validators.minLength(6), Validators.maxLength(15), passwordComplexityValidator()],
    }),
    role: new FormControl<string>('', [Validators.required]),
  }, { validators: passwordMatchValidator });

  register(form: Partial<{
    userName: string | null;
    password: string | null;
    confirmPassword: string | null;
    role: string | null;
    fullName: string | null;
  }> | null) {
    this.diag.open<ConfirmationComponent, {}, boolean>(ConfirmationComponent, {
      data: 'Are you sure you want to register?'
    })
      .afterClosed()
      .pipe(
        filter(x => !!x),
        map(() => (form as RegisterVm)),
        switchMap(x => this.http.register(x)),
      )
      .subscribe(user => {
        this.form.reset();
        this.users.update(x => [...x, {
          fullName: form!.fullName as string,
          userName: form!.userName as string,
          id: user.id,
          role: [user.role]
        }]);
      });
  }

  delUser(id: string) {
    this.diag.open<ConfirmationComponent, {}, boolean>(ConfirmationComponent, {
      data: 'Are you sure you want to delete this user?'
    })
      .afterClosed()
      .pipe(
        filter(x => !!x),
        switchMap(() => this.http.delete(id)),
        switchMap(() => this.snack.open('User deleted successfully', 'Close').afterDismissed())
      )
      .subscribe(() => {
        this.users.reload();
      });
  }
}

export const passwordMatchValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const password = control.get('password');
  const confirm = control.get('confirmPassword');

  if (!password || !confirm) {
    return null; // controls not yet initialized
  }

  return password.value === confirm.value
    ? null
    : { passwordMismatch: true };
};

export function passwordComplexityValidator() {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value)
      return null;
    const value = control.value || '';
    const hasUppercase = /[A-Z]/.test(value);
    const hasLowercase = /[a-z]/.test(value);
    const hasNumber = /\d/.test(value);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(value);
    const isValid = hasUppercase && hasLowercase && hasNumber && hasSpecialChar && value.length >= 8;
    if (isValid)
      return null;
    else {
      if (!hasNumber)
        return { passwordCheck: 'number' };
      else if (!hasUppercase)
        return { passwordCheck: 'uppercase' };
      else if (!hasLowercase)
        return { passwordCheck: 'lowercase' };
      else if (!hasSpecialChar)
        return { passwordCheck: 'specialCharacter' };
    }
    return null;
  }
}
