import {Component, Inject, OnInit} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { ClientesService } from '../clientes.service';
import { Icliente } from '../list-clientes/list-clientes.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-insert-clientes',
  templateUrl: 'insert-clientes.component.html'
})
export class InsertClientesComponent implements OnInit {
  fg: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<InsertClientesComponent>,
    private clientesSrv: ClientesService,
    private fb: FormBuilder,
    ) {
      this.fg = this.fb.group({
        nombre: [null, Validators.required],
        rfc: [null, Validators.required],
        direccion: [null, Validators.required],
        cp: [null, [Validators.required, Validators.maxLength(3)]],
        email: [null, [Validators.required, Validators.email]]
      });
    }

    ngOnInit(): void {
      
    }
  onClose(response: boolean) {
    this.dialogRef.close(response);
  }

  save() {
    const {
      nombre,
      rfc,
      direccion,
      cp,
      email
    } = this.fg.value;
    let  payload: Icliente = {
      nombre: nombre,
      rfc: rfc,
      direccion: direccion,
      cp: cp,
      email: email
    }
    this.clientesSrv.GuardarCliente(payload);
  }
}
