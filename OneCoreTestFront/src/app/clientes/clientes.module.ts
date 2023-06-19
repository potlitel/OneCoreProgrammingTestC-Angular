import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ListClientesComponent } from './list-clientes/list-clientes.component';
import { MatTable } from '@angular/material';

@NgModule({
  declarations: [
    ListClientesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTable
  ]
})
export class ClientesModule { }
