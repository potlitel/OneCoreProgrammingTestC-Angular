import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ImportFileComponent } from './importFile.component';


@NgModule({
  declarations: [
    ImportFileComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
    
  ]
})
export class ImportFileModule { }
