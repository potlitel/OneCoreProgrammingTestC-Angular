import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ImportFileComponent } from './importFile/importFile.component';
import { ListClientesComponent } from './clientes/list-clientes/list-clientes.component';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule } from '@angular/material/paginator';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { UpdateClientesComponent } from './clientes/update-clientes/update-clientes.component';
import { InsertClientesComponent } from './clientes/insert-clientes/insert-clientes.component';
import { IdleComponent } from './components/idle/idle.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { InicioComponent } from './inicio/inicio.component';
import { SearchExcelComponent } from './searchExcel/searchExcel.component';
import { ReportViewerModule } from 'ngx-ssrs-reportviewer';
import { ReportComponent } from './report/report.component';

@NgModule({
  declarations: [					
    AppComponent,
    ImportFileComponent,
    ListClientesComponent,
    UpdateClientesComponent,
    InsertClientesComponent,
    IdleComponent,
    ConfirmComponent,
    InicioComponent,
    SearchExcelComponent,
    ReportComponent
   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatIconModule,
    MatMenuModule,
    MatDialogModule,
    MatSelectModule,
    ReportViewerModule,
    MatPaginatorModule
  ],
  entryComponents: [UpdateClientesComponent,InsertClientesComponent, IdleComponent, ConfirmComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
