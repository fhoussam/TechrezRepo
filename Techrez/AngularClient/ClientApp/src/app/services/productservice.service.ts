import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { IProduct } from './../Models/Product'
import { Observable } from 'rxjs/Observable'

@Injectable()
export class ProductserviceService {

  readonly rootUrl: string;

  constructor(private httpClient : HttpClient)
  {
    this.rootUrl = 'https://localhost:44356/api/products';
  }

  getProducts(): Observable<IProduct[]>{
    return this.httpClient.get<IProduct[]>(this.rootUrl);
  }
}
