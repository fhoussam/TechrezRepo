import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { ProductService } from '../../../../services/product.service';
import { NgRedux, select } from '@angular-redux/store';
import { IAppState } from '../../../../models/appState';
import { SEARCH_PRODUCT } from '../../../../models/constants';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    @Output() searchResultEmitter = new EventEmitter();

    constructor(private productService: ProductService, private ngRedux: NgRedux<IAppState>) {
        this.search();
    }
    ngOnInit() { }

    search() {
        this.productService.getProducts().subscribe(data => {
            let x: any = data;
            this.searchResultEmitter.emit(x);
            this.ngRedux.dispatch({ type: SEARCH_PRODUCT });
        });
    }
}
