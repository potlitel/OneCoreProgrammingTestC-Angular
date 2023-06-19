import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ImportFileService } from './importFile.service';
import * as XLSX from 'xlsx';
const { read, write, utils } = XLSX;
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

type AOA = any[][];

@Component({
  selector: 'app-importFile',
  templateUrl: './importFile.component.html',
  styleUrls: ['./importFile.component.css']
})
export class ImportFileComponent implements OnInit {
  data: AOA = [];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'Libro1.xlsx';
  headers = [];
  columnas = ['Id Cliente','Cantidad', 'Descripción', 'Precio Unitario','Total']
  excelData:any;
  cargado = false;
  excelValidado = true;


  constructor(
    private importFileSrv: ImportFileService,
    private _router: Router
    ) { }

  ngOnInit() {
    
  }

  onFileChange(evt: any) {
    const target: DataTransfer = <DataTransfer>(evt.target);
    if (target.files.length !== 1) throw new Error('Cannot use multiple files');
    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      //leer fichero excel
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

      //Selecciono la primera hoja del libro
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];

      //Convierto a JSON
      this.data = <AOA>(XLSX.utils.sheet_to_json(ws, { header: 1 }));
      let incorrectData = '';
      if (this.data)
      {
        //Validar las columnas en excel
        this.data[0].map(d2=> {
          let indice = this.columnas.findIndex(i=> i === d2);
          if (indice === -1)
          {
            incorrectData = incorrectData + d2 + ',';
            //no encontrado, quiere decir que esta columna no es aceptada
            this.excelValidado = false;
          }
        });
        this.cargado = true;
        if (incorrectData.length > 0)
        {
          incorrectData = incorrectData.substring(0,incorrectData.length-1);
        }
        if (this.excelValidado) {
          Swal.fire({
            title: 'Validando',
            type: 'success',
            text: 'El fichero Excel ha sido importado y validado.',
            confirmButtonText: 'Aceptar'
          });
        } else {
          let textoError = '';
          if (incorrectData.indexOf(',') > 0)
          {
            textoError = 'Los campos "' + incorrectData + '" del fichero Excel no son correctos.';
          } else {
            textoError = 'El campo "' + incorrectData + '" del fichero Excel no es correcto.';
          }
          textoError = textoError + ' No se mostrarán los datos importados del Excel.'
          Swal.fire({
            title: 'Validando',
            type: 'error',
            text: textoError,
            confirmButtonText: 'Aceptar'
          });
        
           this.data = [];
        }
      }
    };
      reader.readAsBinaryString(target.files[0]);
  } 

  GuardarDatos() {
    let i = 0;
    let i2 = 0;
    let idCliente = 0;
    let cantidad = 0;
    let descripcion = '';
    let precioUnitario = 0;
    let total = 0;
    let payload= [];
    this.data.map(d=> {
      if (i !== 0) 
      {
        i2 = 0;
        d.map(d2=>{
          if (i2 === 0)
          {
            idCliente = d2;
          } else if(i2 === 1)
          {
            cantidad = d2;
          } else if(i2 === 2)
          {
            descripcion = d2
          } else if(i2 === 3)
          {
            precioUnitario = d2;
          } else if(i2 === 4)
          {
            total = d2;
          }
          i2 = i2 + 1;
        });
        payload.push(
          {
            idCliente:idCliente,
            cantidad:cantidad,
            descripcion:descripcion,
            precioUnitario:precioUnitario,
            total:total});
      }
      i = i + 1;
    });
    let payl = {
      id: 0,
      nombre: '',
      direcFisica: '',
      estadoNotifi: 'F',
      fecha: new Date(),
      compras: payload
    }
    this.importFileSrv.GuardarExcel(payl);
  }

  gotoInicio() {
    this._router.navigate(['/']);
  }
}
