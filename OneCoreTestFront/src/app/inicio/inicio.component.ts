import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {

  constructor(private _router: Router) { }

  ngOnInit() {
  }

  gotoClientes()
  {
    this._router.navigate(['/client']);
  }
  gotoImportExcel() {
    this._router.navigate(['/import']);
  }

  gotoSearchExcel(){
    this._router.navigate(['/search']);
  }

}
