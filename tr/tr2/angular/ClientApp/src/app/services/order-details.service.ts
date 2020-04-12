import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedList } from '../models/PagedList';
import { SearchOrderDetailsResponse } from '../models/SearchOrderDetails';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  private baseUrl: '/api/orderDetails';

  constructor(private http: HttpClient) { }

  //getOrderDetails(): Observable<PagedList<SearchOrderDetailsResponse>> {
  //  return this.http.get<PagedList<SearchOrderDetailsResponse>>(this.baseUrl, );
  //}
  //getOrderDetailsForEdit() {

  //}
  //getOrderDetailsForDisplay() {

  //}
  //deleteOrderDetails() {

  //}
}
