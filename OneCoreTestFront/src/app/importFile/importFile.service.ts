import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import Swal from 'sweetalert2';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImportFileService {
  res: {
    data: any[],
    message: string,
    ok: boolean
  } = {
      data: [],
      message: "",
      ok: false
  }
  private api = environment.api;
  private excelData$ = new BehaviorSubject<any>([]);
  constructor(private http: HttpClient) { }

  getexcelData(): Observable<any> {
    return this.excelData$;
  }
  
  public GuardarExcel(data: any) {
    this.res = {
      data: [],
      message: "",
      ok: false
    }
    this.http.post<any>(this.api + "api/Documentos", data)
    .subscribe({
      next: data => {
        this.res.data = data;
        this.excelData$.next(this.res.data);
        Swal.fire("Validando", "Registro Guardado. El identificador único devuelto fue:" + data.id, "success");
    },
    error: error => {
      Swal.fire("Error!", error.message, "error");
    }
    });
    
  }

}

