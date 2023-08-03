import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from '../auth/auth.service';
import { ErrorDialogService } from './error-dialog.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(
    private errorDialogService: ErrorDialogService,
    private authService: AuthService,
    private zone: NgZone,
    private dialogRef: MatDialog
  ) {}

  handleError(error: any) {
    console.log(error);
    if (error.error && !error.error.statusCode) {
      error = new Error(
        "You are unable to reach our servers. Maybe our server isn't running, or you are offline!"
      );
    } else if (error.status === 401) {
      this.zone.run(() => this.authService.logout());
      this.dialogRef.closeAll();
      return;
    } else if (error.error && error.error.message) {
      error = new Error(error.error.message);
    }
    this.zone.run(() =>
      this.errorDialogService.openDialog(error?.message || 'Undefined client error')
    );
  }
}
