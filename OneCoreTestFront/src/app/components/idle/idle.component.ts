import {Component, Inject, OnInit} from '@angular/core';
import {MatDialogRef} from '@angular/material';
import { interval } from 'rxjs';

@Component({
  selector: 'app-idle',
  templateUrl: 'idle.component.html'
})
export class IdleComponent implements OnInit {
  interval: number;
  intervalPart: any;
  constructor(public dialogRef: MatDialogRef<IdleComponent>) {}
  ngOnInit(): void {
    this.intervalPart = interval(500);
    this.intervalPart.subscribe((res)=> {
         console.log(res);
       });
  }
  onClose(response: boolean) {
    this.intervalPart.unsubscribe();
    this.dialogRef.close(response);
  }
}
