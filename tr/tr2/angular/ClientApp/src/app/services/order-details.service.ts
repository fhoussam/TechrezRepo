import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedList } from '../models/PagedList';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHelperService } from '../shared-module/services/http-helper';
import { EditOrderDetailCommand, OrderDetails, SearchOrderDetailsResponse, SearchOrderDetailsQuery } from '../models/order-details-models';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { DatePipe } from '@angular/common';
import { APP_SETTINGS } from '../shared-module/models/APP_SETTINGS';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  private baseUrl: '/api/orderDetails';

  constructor(
    private http: HttpClient,
    private httpHelper: HttpHelperService,
    public datepipe: DatePipe,
  ) { }

  searchOrderDetails(searchOrderDetailsQuery: SearchOrderDetailsQuery) {
    let Params = new HttpParams();
    Params = Params.append('orderDateFrom', this.datepipe.transform(searchOrderDetailsQuery.orderDateFrom, APP_SETTINGS.queryStringDateFormat));
    Params = Params.append('orderDateTo', this.datepipe.transform(searchOrderDetailsQuery.orderDateTo, APP_SETTINGS.queryStringDateFormat));
    Params = Params.append('productId', searchOrderDetailsQuery.productId.toString());
    return this.http.get('/api/orderDetails?', { params: Params });
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
