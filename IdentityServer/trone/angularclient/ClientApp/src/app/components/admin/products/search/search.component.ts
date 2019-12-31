import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { SEARCH_PRODUCT } from '../../../../models/constants';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { productSearchParam } from '../../../../models/productSearchParam';
import { urlToProperty } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    @Output() searchResultEmitter = new EventEmitter();
    categoryId: string;
    categories: category[];
    searchParams: productSearchParam
    constructor(
        private productService: ProductService,
        private feedService: FeedService,
    ) {
        if (location.search) {
            this.searchParams = urlToProperty(location.search);
            if (this.searchParams) {
                this.search();
            }
        }
        else
            this.searchParams = new productSearchParam();

        this.categories = APP_SETTINGS.categories;
    }
    ngOnInit() { }

    search() {
        this.productService.getProducts().subscribe(data => {
            let x: any = data;
            this.searchResultEmitter.emit(x);
            this.feedService.add(SEARCH_PRODUCT);
        });
    }
}
