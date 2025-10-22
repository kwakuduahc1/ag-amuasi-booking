import { HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class BsHandler {

  private snack = inject(MatSnackBar);

  onError(err: HttpErrorResponse): void {
    console.log('Error occurred', err);
    if (err instanceof HttpErrorResponse) {
      let message = '';
      if (err.statusText === 'Unknown Error') {
        message = 'An error occurred and the developer has been notified'
      }
      switch (err.status) {
        case 500:
          message = err.error.message || '';
          break;
        case 400:
          message = err.error.message || 'Try again or contact the administrator';
          break;
        case 401:
          message = 'You need to sign in';
          break;
        case 403:
          message = 'You are not allowed to view this';
          break;
        case 404:
          message = err.error.message || 'The server cannot find your request';
          break;
        case 409:
          message = err.error.message || 'There is a conflict with your request';
      }
      if (message) {
        this.snack.open(message, 'Dismiss', {
          panelClass: 'snackbar-error-light'
        });
        return
      }
      else if (err.error.message || err.message) {
        message = err.error.message || err.message;
      }
      else if (err.status >= 500) {
        message = 'The was a fatal error. Please try again';
      }
      else if (!err.error) {
        message = 'Not yet completed';
      }
      else {
        message = err.statusText;
      }
      this.snack.open(message, 'Dismiss', {
        panelClass: 'snackbar-error',
        announcementMessage: 'OO'
      });
    }
  }
}
