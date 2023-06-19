import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Icliente } from "./list-clientes/list-clientes.component";
import Swal from "sweetalert2";

@Injectable({
  providedIn: "root",
})
export class ClientesService {
  private api = environment.api;
  public clientes: Icliente[] = [];
  res: {
    data: any[];
    message: string;
    ok: boolean;
  } = {
    data: [],
    message: "",
    ok: false,
  };

  public clientesData$ = new BehaviorSubject<any>([]);
  public fg: FormGroup;
  constructor(private http: HttpClient) {
    this.fg = new FormGroup({
      id: new FormControl("", [Validators.required]),
      nombre: new FormControl("", [Validators.required]),
      rfc: new FormControl("", [Validators.required]),
      direccion: new FormControl("", [Validators.required]),
      cp: new FormControl("", [Validators.required]),
      email: new FormControl("", [Validators.required]),
    });
  }

  getClienteData(): Observable<any> {
    return this.clientesData$;
  }

  public GetClientes() {
    this.res = {
      data: [],
      message: "",
      ok: false,
    };
    this.http.get<any>(this.api + "api/Clientes").subscribe({
      next: (data) => {
        this.res.data = data;
        this.clientes = data;
        this.clientesData$.next(this.res.data);
      },
      error: (error) => {
        Swal.fire("Error!", error.message, "error");
      },
    });
  }

  public GetCliente(id: any) {
    this.res = {
      data: [],
      message: "",
      ok: false,
    };
    this.http.get<any>(`${this.api}getCliente?id=${id}`).subscribe({
      next: (data) => {
        this.res.data = data;
        this.clientesData$.next(this.res.data);
      },
      error: (error) => {
        Swal.fire("Error!", error.message, "error");
      },
    });
  }
  public GuardarCliente(data: any) {
    this.res = {
      data: [],
      message: "",
      ok: false,
    };
    this.http.post<any>(this.api + "api/Clientes", data).subscribe({
      next: (data) => {
        this.res.data = data;
        Swal.fire("Validando", "Registro insertado!", "success");
        this.clientes = this.res.data;
        this.clientesData$.next(this.res.data);
      },
      error: (error) => {
        Swal.fire("Error!", error.message, "error");
      },
    });
  }

  public EliminarCliente(id: any) {
    this.res = {
      data: [],
      message: "",
      ok: false,
    };
    Swal.fire({
      title: "Validando",
      type: "question",
      text: "Está seguro de eliminar el registro?",
      showCancelButton: true,
      showConfirmButton: true,
      confirmButtonText: "Si",
      cancelButtonText: "No",
    }).then((result) => {
      if (!result.dismiss) {
        this.http.delete<any>(`${this.api}api/Clientes/${id}`).subscribe({
          next: (data) => {
            this.res.data = data;
            this.clientes = this.res.data;
            this.clientesData$.next(this.res.data);
            Swal.fire("Eliminado!", "Registro eliminado!", "success");
          },
          error: (error) => {
            Swal.fire("Error!", error.message, "error");
          },
        });
      }
    });
  }

  public ActualizarCliente(id: number, data: any) {
    this.res = {
      data: [],
      message: "",
      ok: false,
    };
    this.http.put<any>(`${this.api}api/Clientes/${id}`, data).subscribe({
      next: (data) => {
        this.res.data = data;
        Swal.fire("Validando", "Registro actualizado!", "success");
        this.clientes = this.res.data;
        this.clientesData$.next(this.res.data);
      },
      error: (error) => {
        Swal.fire("Error!", error.message, "error");
      },
    });
  }
}
