import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { UsersComponent } from './components/users/users.component';
import { UserHttpService } from '../http/user-http-service';
import { LoginGuard } from '../guards/LoginGuard';

export const AUTH_ROUTES: Routes = [
  {
    path: 'login',
    title: 'Login',
    component: LoginComponent
  },
  {
    path: 'users',
    component: UsersComponent,
    resolve: {
      users: () => inject(UserHttpService).users()
    },
    canActivate: [LoginGuard]
  },
  {
    path: 'register',
    component: RegisterComponent,
    resolve: {
      roles: () => ["Cashier", "Receiver", "Accountant"]
    }
  }
];
