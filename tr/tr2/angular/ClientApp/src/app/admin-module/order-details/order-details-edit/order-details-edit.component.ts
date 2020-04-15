import { Component, OnInit } from '@angular/core';
import { IAppState } from '../../../shared-module/reducers/shared-reducer-selector';
import { Store } from '@ngrx/store';
import { FormGroup, FormControl } from '@angular/forms';
import { DropDownListData, KeyValue, DropDownListIdentifier } from '../../../models/config-models';
import { EditOrderDetailCommand } from '../../../models/order-details-models';
import { EditOrderDetailsBegin } from '../order-details-reducer/order-details-actions';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-details-edit',
  templateUrl: './order-details-edit.component.html',
  styleUrls: ['./order-details-edit.component.css']
})
export class OrderDetailsEditComponent implements OnInit {

  form: FormGroup;
  customers: KeyValue[];
  employees: KeyValue[];
  countries: KeyValue[];

  constructor(
    private store: Store<IAppState>,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {

    this.store.select('orderDetailsState').subscribe(x => {
      
      if (x && x.isEditMode && x.selectedItemForEdit) {

        const formData = x.selectedItemForEdit;
        this.customers = formData.dropDownListData[DropDownListIdentifier.Customers];
        this.employees = formData.dropDownListData[DropDownListIdentifier.Employees];
        this.countries = formData.dropDownListData[DropDownListIdentifier.Countries];

        this.form = new FormGroup({
          'orderId': new FormControl(formData.orderId),
          'productId': new FormControl(this.route.snapshot.paramMap.get('id')),
          'quantity': new FormControl(formData.quantity),
          'customerId': new FormControl(formData.customerId),
          'employeeId': new FormControl(formData.employeeId),
          'orderDate': new FormControl(formData.orderDate),
          'requiredDate': new FormControl(formData.requiredDate),
          'shippedDate': new FormControl(formData.shippedDate),
          'shipAddress': new FormControl(formData.shipAddress),
          'shipCity': new FormControl(formData.shipCity),
          'shipRegion': new FormControl(formData.shipRegion),
          'shipPostalCode': new FormControl(formData.shipPostalCode),
          'shipCountry': new FormControl(formData.shipCountry),
        });
      }
    });

  }

  submit() {
    const editCommand = this.form.value as EditOrderDetailCommand;
    this.store.dispatch(new EditOrderDetailsBegin(editCommand));
  }

  reset() {

  }
}
