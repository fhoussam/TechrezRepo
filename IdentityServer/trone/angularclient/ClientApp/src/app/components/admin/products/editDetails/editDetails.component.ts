import { Component, OnInit } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../../../../models/appState';
import { SAVE_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgModel } from '@angular/forms'
import { adminProductEdit } from '../../../../models/adminProductEdit';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductService } from '../../../../services/product.service';

@Component({
    selector: 'app-details',
    templateUrl: './editDetails.component.html',
    styleUrls: ['./editDetails.component.css']
})
export class EditDetailsComponent implements OnInit {

    selectedItem: adminProductListItem;
    product: adminProductEdit;
    categories: category[];
    selectedFile: File = null;

    fileSelected(event) {
        this.selectedFile = <File>event.target.files[0];
    }

    constructor(
        private productEventEmitter: ProductEventEmitterService,
        private ngRedux: NgRedux<IAppState>,
        private productService: ProductService,
    ) {
        this.categories = APP_SETTINGS.categories;
        this.selectedItem = new adminProductListItem();
        this.productEventEmitter.cast.subscribe(selectedItem => {
            this.selectedItem = selectedItem;

            this.product = new adminProductEdit();
            this.product.code = this.selectedItem.code;
            this.product.categoryId = this.selectedItem.categoryId;
            this.product.description = this.selectedItem.description;
            this.product.price = this.selectedItem.price;
            this.product.quantity = this.selectedItem.quantity;
        });
    }

    ngOnInit() { }

    saveChanges() {
        this.productService.save(this.product, this.selectedFile).subscribe((resp:any) => {
            this.selectedItem.price = this.product.price;
            this.selectedItem.description = this.product.description;
            if (resp.path) this.selectedItem.photoUrl = resp.path;
            this.selectedItem.categoryId = this.product.categoryId;
            this.selectedItem.quantity = this.product.quantity;
            this.ngRedux.dispatch({ type: SAVE_PRODUCT });
        });
    }
}
