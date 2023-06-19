import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-confirm',
  templateUrl: 'confirm.component.html'
})
export class ConfirmComponent {
  constructor(public dialogRef: MatDialogRef<ConfirmComponent>) {}
  onClose(response: boolean) {
    this.dialogRef.close(response);
  }
}
