import { Component, OnInit } from '@angular/core';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { Store } from '@ngrx/store';
import { SearchOrderDetailsBegin } from '../order-details-reducer/order-details-actions';
import { SearchOrderDetailsQuery } from '../../../models/order-details-models';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-order-details-search',
  templateUrl: './order-details-search.component.html',
  styleUrls: ['./order-details-search.component.css']
})
export class OrderDetailsSearchComponent implements OnInit {

  constructor(
    private store: Store<IAppState>,
    public datepipe: DatePipe,
  ) { }

  ngOnInit(): void {
  }

  submit() {
    const query = new SearchOrderDetailsQuery();
    query.orderDateFrom = new Date(1996, 1, 1);
    query.orderDateTo = new Date(1997, 12, 1);
    query.productId = 59;
    this.store.dispatch(new SearchOrderDetailsBegin(query));
  }
}
