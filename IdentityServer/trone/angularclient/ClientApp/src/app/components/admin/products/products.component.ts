import { Component, OnInit, Input, Injector, NgZone } from '@angular/core';
import { adminProductListItem } from '../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../services/product-event-emitter.service';
import { Router, NavigationExtras } from '@angular/router';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

    constructor(
        private productEventEmitter: ProductEventEmitterService,
        private router: Router,
        private injector: Injector
    ) {
        this.productEventEmitter.cast.subscribe(selectedItem => {
            if (selectedItem.code) {
                this.selectedItemCode = selectedItem.code;
            }
        });
    }
    searchResult: adminProductListItem[] = [];
    selectedItemCode: string = null;

    ngOnInit() {
    }

    displaySearchResult(searchResult) {
        this.searchResult = searchResult;
    }
}
