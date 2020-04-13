import { Component, OnInit } from '@angular/core';
import { SearchOrderDetailsResponse } from '../../../models/order-details-models';
import { OrderDetailsState } from '../order-details-reducer/order-details-reducer';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { Store } from '@ngrx/store';
import { PagedList } from '../../../models/PagedList';

@Component({
  selector: 'app-order-details-list',
  templateUrl: './order-details-list.component.html',
  styleUrls: ['./order-details-list.component.css']
})
export class OrderDetailsListComponent implements OnInit {

  list: PagedList<SearchOrderDetailsResponse>;
  selectedItem: number;

  constructor(
    private store: Store<IAppState>,
  ) { }

  ngOnInit(): void {
    this.store.select('orderDetailsState').subscribe((x: OrderDetailsState) => {
      if (x) {
        this.list = x.list;
        this.selectedItem = x.selectedItem;
      }
    });
  }
}
