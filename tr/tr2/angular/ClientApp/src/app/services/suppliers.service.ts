import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { ISupplier } from '../models/ISupplier';

@Injectable({
  providedIn: 'root'
})
export class SuppliersService {

  constructor(private http: HttpClient) { }

  getSuppliers():Observable<ISupplier[]> {
    return this.http.get<ISupplier[]>('api/suppliers');
  }
}
