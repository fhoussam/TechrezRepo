import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { productSearchResult, product } from '../models/product';
import { Observable } from 'rxjs'
import { productFilter } from '../models/productfilter';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  //to consider : use intereceptor
  headers:HttpHeaders;
  constructor(private httpClient : HttpClient) {
    this.headers = new HttpHeaders();
    this.headers = this.headers
      .set('Content-Type', 'application/json')
      .set('Accept', 'application/json');
  }

  private static rootUrl(destination:string):string{ return "https://localhost:44356/api/products/" + destination }

  getProducts(productFilter : productFilter):Observable<productSearchResult>{
    return this.httpClient.post<productSearchResult>(ProductService.rootUrl("SearchProduts"), productFilter, {headers: this.headers});
  }

  getProduct(productid:number):Observable<product>{
    return this.httpClient.get<product>(ProductService.rootUrl(productid.toString()));
  }

  updateProduct(product: product): Observable<product> {
    return this.httpClient.put<product>(ProductService.rootUrl(""), product);
  }

  addProduct(product: product): Observable<product> {
    return this.httpClient.post<product>(ProductService.rootUrl(""), product);
  }

  deleteProduct(productId:number){
    return this.httpClient.request('delete', ProductService.rootUrl(""), ({ headers : this.headers, body:productId }));
  }
}
