import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { SEARCH_PRODUCT } from '../../../../models/constants';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductSearchParams } from '../../../../models/productSearchParam';
import { urlToProperty } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';
import { Router } from '@angular/router';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    @Output() searchResultEmitter = new EventEmitter();
    categoryId: string;
    categories: category[];
    searchParams: ProductSearchParams

    constructor(
        private productService: ProductService,
        private feedService: FeedService,
        private route: Router,
    ) {
        if (location.search) {
            this.searchParams = urlToProperty(location.search);
            if (this.searchParams) {
                this.search();
            }
        }
        else
            this.searchParams = new ProductSearchParams();

        this.categories = APP_SETTINGS.categories;
    }
    ngOnInit() {
        this.searchParams.CategoryId = 2;
        this.searchParams.Description = "Asus";
    }

    search() {
        this.productService.getProducts(this.searchParams).subscribe(data => {

            let x: any = data;
            this.searchResultEmitter.emit(x);
            this.feedService.add(SEARCH_PRODUCT);

            this.route.navigate([], {
                queryParams: {
                    categoryId: this.searchParams.CategoryId,
                    description: this.searchParams.Description,
                },
                queryParamsHandling: 'merge',
            });
        });
    }
}
