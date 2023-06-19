import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormControl, Validators } from '@angular/forms';
import { SearchExcelService } from './searchExcel.service';
import { ReportService } from '../report/report.service';
import pdfMake from "pdfmake/build/pdfmake";  
import pdfFonts from "pdfmake/build/vfs_fonts";  
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-searchExcel',
  templateUrl: './searchExcel.component.html',
  styleUrls: ['./searchExcel.component.css']
})
export class SearchExcelComponent implements OnInit {
  public fg: FormGroup;
  compras = [];
  documentos = [];
  selectedCompra = 0;
  tableDetail = ['1', '2','bgbgbgb' ,'2.40', '4.80'];
  idReporte = 5;
  body = [];
  constructor(private _router: Router,
    private searchSrv: SearchExcelService, private reportSrv: ReportService) { 
    this.fg = new FormGroup({
      IdCompra: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit() {
    this.searchSrv.comprasData$.subscribe(cd=> {
      this.compras = cd.data;
    });
    this.searchSrv.documentosData$.subscribe(d=> {
      this.documentos = d;
    });
    if (this.searchSrv.compras.length === 0)
    {
      this.searchSrv.GetCompras();
    }
    if (this.searchSrv.documentos.length === 0)
    {
      this.searchSrv.GetDocumentos();
    }
    //Aqui tengo que hacer un get de compras
    //y llenar el body 
    this.reportSrv.getReportData().subscribe(d=> {
      if (d.length > 0)
      {
        this.body = [['Id Cliente', 'Cantidad', 'Descripción', 'PrecioUnitario', 'Total']];
        d.map(d2=> {
          this.body.push([
            d2.idCliente,d2.cantidad,d2.descripcion,d2.precioUnitario,d2.total
          ]);
        });
      }
      if (this.body.length > 0) {
        this.generatePDF(this.selectedCompra);
      }
    });
    // if (this.reportSrv.compra.length === 0)
    // {
    //   this.reportSrv.getPDFData(4);
    // }
  }

  gotoInicio() {
    this._router.navigate(['/']);
  }

  MostrarReporte() {
    this.reportSrv.getPDFData(this.selectedCompra);
  }

  selectChange(id) {
    this.selectedCompra = parseInt(id);
  }

  generatePDF(id) {  
    let docDefinition = {  
      content: [ 
        {
          text: 'Compras del Documento No.' + id, 
          style: 'header'
        },
        {
          style: 'tableExample',
          table: {
            body: this.body
          }
        }
      ],
      styles: {
        header: {
          fontSize: 18,
          bold: true,
          margin: [0, 0, 0, 10]
        },
        subheader: {
          fontSize: 16,
          bold: true,
          margin: [0, 10, 0, 5]
        },
        tableExample: {
          margin: [0, 5, 0, 15]
        },
        tableHeader: {
          bold: true,
          fontSize: 13,
          color: 'black'
        }
      },
      defaultStyle: {
        // alignment: 'justify'
      }
    };  
    pdfMake.createPdf(docDefinition).open();  
  }

}
