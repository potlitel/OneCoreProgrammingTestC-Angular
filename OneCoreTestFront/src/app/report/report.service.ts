import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import Swal from 'sweetalert2';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  res: {
    data: any[],
    message: string,
    ok: boolean
  } = {
      data: [],
      message: "",
      ok: false
  }
  public compra = [];
  public selectedCompra: number = 0;
  private api = environment.api;
  private reportData$ = new BehaviorSubject<any>([]);
  constructor(private http: HttpClient, private reportSrv: ReportService) { }

  getReportData(): Observable<any> {
    return this.reportData$;
  }

  public getPDFData(id)
  {
    this.res = {
      data: [],
      message: "",
      ok: false
    }
    this.http.get<any>(`${this.api}ComprasXDocumento/${id}`)
      .subscribe({
        next: data => {
          this.compra = data;
          this.reportData$.next(this.compra);
      },
      error: error => {
        Swal.fire("Error!", error.message, "error");
      }
    });
  }

}
