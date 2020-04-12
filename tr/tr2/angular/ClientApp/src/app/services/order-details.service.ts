import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedList } from '../models/PagedList';
import { SearchOrderDetailsResponse, SearchOrderDetailsQuery, OrderDetails, EditOrderDetailCommand } from '../models/SearchOrderDetails';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHelperService } from '../shared-module/services/http-helper';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  private baseUrl: '/api/orderDetails';

  constructor(private http: HttpClient, private httpHelper: HttpHelperService) { }

  searchOrderDetails(searchOrderDetailsQuery: SearchOrderDetailsQuery): Observable<PagedList<SearchOrderDetailsResponse>> {
    return this.http.get<PagedList<SearchOrderDetailsResponse>>
      (this.baseUrl, { params: this.httpHelper.toHttpParams(searchOrderDetailsQuery) });
  }

  getOrderDetails(orderId: number, productId: number, forEdit: boolean): Observable<OrderDetails> {
    const params = new HttpParams();
    params.append('orderId', orderId.toString());
    params.append('productId', productId.toString());
    params.append('forEdit', forEdit.toString());
    return this.http.get<OrderDetails>(this.baseUrl, { params: params });
  }

  deleteOrderDetails(orderId: number, productId: number): Observable<void> {
    const params = new HttpParams();
    params.append('orderId', orderId.toString());
    params.append('productId', productId.toString());
    return this.http.delete<void>(this.baseUrl, { params: params });
  }

  editOrderDetails(editOrderDetailCommand: EditOrderDetailCommand) {
    return this.http.post(this.baseUrl, editOrderDetailCommand);
  }
}
