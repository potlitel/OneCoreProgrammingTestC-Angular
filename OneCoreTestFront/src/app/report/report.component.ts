import { Component, OnInit } from '@angular/core';
import pdfMake from "pdfmake/build/pdfmake";  
import pdfFonts from "pdfmake/build/vfs_fonts";  
import { ReportService } from './report.service';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {
 
  tableDetail = ['1', '2','bgbgbgb' ,'2.40', '4.80'];
  idReporte = 5;
  body = [
    ['Id Cliente', 'Cantidad', 'Descripción', 'PrecioUnitario', 'Total']
  ]
  constructor(private reportSrv: ReportService) { }

  ngOnInit() {
    //Aqui tengo que hacer un get de compras
    //y llenar tableDetail 
    this.reportSrv.getReportData().subscribe(d=> {
      if (d.length > 0)
      {
        d.map(d2=> {
          this.body.push([
            d2.idCliente,d2.cantidad,d2.descripcion,d2.precioUnitario,d2.total
          ]);
        });
      }
    });
    if (this.reportSrv.compra.length === 0)
    {
      this.reportSrv.getPDFData(4);
    }
  }

  generatePDF() {  
    let docDefinition = {  
      content: [ 
        {
          text: 'Compras del Documento No.' + this.idReporte, 
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
