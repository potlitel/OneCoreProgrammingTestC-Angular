import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListClientesComponent } from './clientes/list-clientes/list-clientes.component';
import { ImportFileComponent } from './importFile/importFile.component';
import { InicioComponent } from './inicio/inicio.component';
import { ReportComponent } from './report/report.component';
import { SearchExcelComponent } from './searchExcel/searchExcel.component';

const routes: Routes = [
  {
    path: '', component: InicioComponent
  },
  {
    path: 'import', component: ImportFileComponent
  },
  {
    path: 'client', component: ListClientesComponent
  },
  {
    path: 'search', component: SearchExcelComponent
  },
  {
    path: 'report', component: ReportComponent
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
