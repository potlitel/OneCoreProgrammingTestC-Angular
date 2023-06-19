import {Component, Inject, OnInit} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { ClientesService } from '../clientes.service';
import { Icliente } from '../list-clientes/list-clientes.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-clientes',
  templateUrl: 'update-clientes.component.html'
})
export class UpdateClientesComponent implements OnInit {
  fg: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<UpdateClientesComponent>,
    private clientesSrv: ClientesService,
    private fb: FormBuilder,
    ) {
      this.fg = this.fb.group({
        id: [null, Validators.required],
        nombre: [null, Validators.required],
        rfc: [null, Validators.required],
        direccion: [null, Validators.required],
        cp: [null, [Validators.required, Validators.maxLength(3)]],
        email: [null, [Validators.required, Validators.email]]
      });
    }

    ngOnInit(): void {
      this.fg = this.clientesSrv.fg;
    }
  onClose(response: boolean) {
    this.dialogRef.close(response);
  }

  save() {
    const {
      id,
      nombre,
      rfc,
      direccion,
      cp,
      email
    } = this.fg.value;
    let  payload: Icliente = {
      id: id,
      nombre: nombre,
      rfc: rfc,
      direccion: direccion,
      cp: cp,
      email: email
    }
    this
    this.clientesSrv.ActualizarCliente(payload.id,payload);
  }
}
