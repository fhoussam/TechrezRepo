import { Component, OnInit } from '@angular/core';
import { SearchOrderDetailsResponse } from '../../../models/order-details-models';
import { OrderDetailsState } from '../order-details-reducer/order-details-reducer';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { Store } from '@ngrx/store';
import { PagedList } from '../../../models/PagedList';
import { GridField } from '../../../models/GridField';

@Component({
  selector: 'app-order-details-list',
  templateUrl: './order-details-list.component.html',
  styleUrls: ['./order-details-list.component.css']
})
export class OrderDetailsListComponent implements OnInit {

  gridFields: GridField[];
  selectedItemId: number;
  list: PagedList<SearchOrderDetailsResponse>;
  selectedItem: number;

  constructor(
    private store: Store<IAppState>,
  ) { }

  ngOnInit(): void {

    this.gridFields = [
      new GridField("orderId", "Order Id", 0, true),
      new GridField("companyName", "Company Name", 1, false),
      new GridField("employeeFullName", "Employee", 2, false),
      new GridField("orderDate", "Order Date", 4, false),
      new GridField("ShipCountry", "Quantity Per Unit", 5, false),
    ];

    this.store.select('orderDetailsState').subscribe((x: OrderDetailsState) => {
      if (x) {
        this.list = x.list;
        if (x.selectedItem)
          this.selectedItem = x.selectedItem;
      }
    });
  }
}
