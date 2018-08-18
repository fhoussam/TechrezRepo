import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Product } from './../Models/Product'
import { Observable } from 'rxjs/Observable'

@Injectable()
export class ProductserviceService {

  readonly rootUrl: string;

  constructor(private httpClient : HttpClient)
  {
    this.rootUrl = 'https://localhost:44356/api/products';
  }

  getProducts(): Observable<Product[]>{
    return this.httpClient.get<Product[]>(this.rootUrl);
  }

  deleteProduct(productid: number): Observable<any> {
    return this.httpClient.delete(this.rootUrl + '/' + productid);
  }

  updateProduct(product: Product): Observable<Product> {
    return this.httpClient.put<Product>(this.rootUrl, product);
  }
}
