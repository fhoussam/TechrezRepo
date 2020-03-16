import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IProductSearchResponse } from '../models/IProductSearchResponse';
import { SearchProductQuery } from '../models/IProductSearchQuery';
import { IProductDetails } from '../models/IProductDetails';
import { EditProductQuery } from './IEditProductQuery';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  public toHttpParams(object: any): HttpParams {
    var params = new HttpParams()
    Object.keys(object).forEach(function (item) {
      if (object[item])
        params = params.set(item, object[item]);
    });

    return params
  }

  public getProducts(searchProductQuery: SearchProductQuery): Observable<IProductSearchResponse> {
    return this.http.get<IProductSearchResponse>('api/products', {
      headers: new HttpHeaders({ 'content-type': 'application/json' }),
      params: this.toHttpParams(searchProductQuery)
    });
  }

  public getProduct(productId: number): Observable<IProductDetails> {
    return this.http.get<IProductDetails>('api/products/' + productId, {
      headers: new HttpHeaders({ 'content-type': 'application/json' }),
    });
  }

  public editProduct(editProductQuery: EditProductQuery) {
    return this.http.post('api/products', editProductQuery);
  }
}
