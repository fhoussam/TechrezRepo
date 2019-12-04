import { Component, OnInit } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../../../../models/appState';
import { SAVE_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-details',
    templateUrl: './editDetails.component.html',
    styleUrls: ['./editDetails.component.css']
})
export class EditDetailsComponent implements OnInit {

    selectedItem: adminProductListItem;
    constructor(
        private productEventEmitter: ProductEventEmitterService,
        private ngRedux: NgRedux<IAppState>,
        private router: Router,
        private _location: Location,
    ) {
        //let id: string = this.route.snapshot.paramMap.get('id');
        this.productEventEmitter.cast.subscribe(selectedItem => this.selectedItem = selectedItem);
    }

    ngOnInit() { }

    saveChanges() {
        this.selectedItem.price += 10;
        this.ngRedux.dispatch({ type: SAVE_PRODUCT });
    }
}
