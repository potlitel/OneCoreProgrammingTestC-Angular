import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SearchExcelService {
  private api = environment.api;
  res: {
    data: any[],
    message: string,
    ok: boolean
  } = {
      data: [],
      message: "",
      ok: false
  }

  public compras = [];
  public documentos = [];
  public comprasData$ = new BehaviorSubject<any>([]);
  public documentosData$ = new BehaviorSubject<any>([]);
constructor(private http: HttpClient) { }

  getCompraData(): Observable<any> {
    return this.comprasData$;
  }
  getDocumentoData(): Observable<any> {
    return this.documentosData$;
  }
  public MostrarReporte(id)
  {
    this.res = {
      data: [],
      message: "",
      ok: false
    }
    //this.http.get<any>(`${this.api}/api/Compras/documento/${id}`)
    this.http.get<any>(`${this.api}api/Compras`)
    .subscribe({
      next: data => {
        this.res.data = data;
      },
      error: error => {
        Swal.fire("Error!", error.message, "error");
      }
    });
  }

  public GetCompras() {
    this.res = {
      data: [],
      message: "",
      ok: false
    }
    this.http.get<any>(this.api + "api/Compras")
    .subscribe({
      next: data => {
        this.res.data = data;
        this.compras = data;
        this.comprasData$.next(this.compras);
    },
    error: error => {
      Swal.fire("Error!", error.message, "error");
    }
    });
    
  }
  public GetDocumentos() {
    this.res = {
      data: [],
      message: "",
      ok: false
    }
    this.http.get<any>(this.api + "api/Documentos")
    .subscribe({
      next: data => {
        this.res.data = data;
        this.documentos = data;
        this.documentosData$.next(this.documentos);
    },
    error: error => {
      Swal.fire("Error!", error.message, "error");
    }
    });
    
  }
}
