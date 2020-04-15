import { Component, OnInit } from '@angular/core';
import { GetOrderDetailsForDisplayResponse } from '../../../models/order-details-models';
import { Store } from '@ngrx/store';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { SelectOrderDetailsBegin } from '../order-details-reducer/order-details-actions';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-details-info',
  templateUrl: './order-details-info.component.html',
  styleUrls: ['./order-details-info.component.css']
})
export class OrderDetailsInfoComponent implements OnInit {

  constructor(
    private store: Store<IAppState>,
    private activatedRoute: ActivatedRoute,
  ) { }

  item: GetOrderDetailsForDisplayResponse;

  ngOnInit(): void {
    this.store.select('orderDetailsState').subscribe((x) => {
      if (x && x.selectedItemForDisplay) {
        this.item = x.selectedItemForDisplay;
      }
    });
  }

  edit() {
    this.store.dispatch(new SelectOrderDetailsBegin(this.item.orderId, +this.activatedRoute.snapshot.paramMap.get('id'), true));
  }
}
