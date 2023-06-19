import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatPaginator, MatTableDataSource } from '@angular/material';
import { ModalService } from '../../modal/modal.service';
import { UpdateClientesComponent } from '../update-clientes/update-clientes.component'
import { InsertClientesComponent } from '../insert-clientes/insert-clientes.component';
import { ClientesService } from '../clientes.service';
import { Router } from '@angular/router';
import { ViewChild } from '@angular/core';


export interface Icliente {
  id?: number;
  nombre?: string;
  rfc?: string;
  direccion?: string;
  cp?: string;
  email?: string;
}

@Component({
  selector: 'app-list-clientes',
  templateUrl: './list-clientes.component.html',
  styleUrls: ['./list-clientes.component.css']
})
export class ListClientesComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  dialogRef: MatDialogRef<any>;
  clientes: Icliente[] = [];

  displayedColumns: string[] = ['id', 'nombre', 'rfc', 'direccion', 'cp', 'email'];
  dataSource = [];
  modalActualizar = false;
  constructor(
    private modalPopupService: ModalService,
    private clientesSrv: ClientesService,
    private _router: Router
    ) { }

  ngOnInit() {
    if (this.clientesSrv.clientes.length === 0)
    {
      this.clientesSrv.GetClientes();
    }
    this.clientesSrv.clientesData$.subscribe(cd=> {
      this.clientes = cd.data;
      this.dataSource = this.clientes;
    });
  }

  Actualizar(id) {
    let payload = this.dataSource.find(d=> d.id === id);
    if (payload)
    {
      this.clientesSrv.fg.get('id').setValue(payload.id)
      this.clientesSrv.fg.patchValue(payload);
      this.openDialog(UpdateClientesComponent);
    }
  }

  Eliminar(id) {
    this.clientesSrv.EliminarCliente(id);
  }

  InsertarDatos() {
    this.openDialog(InsertClientesComponent);
  }

  openDialog(component: any) {
    this.dialogRef = this.modalPopupService.openPopup<any>(component, null);
    this.dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    });
  }

  gotoInicio() {
    this._router.navigate(['/']);
  }

}
