import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { SEARCH_PRODUCT } from '../../../../models/constants';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductSearchParams } from '../../../../models/productSearchParam';
import { urlToProperty } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';
import { Router, ActivatedRoute } from '@angular/router';

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
        private router: Router,
        private activatedRoute: ActivatedRoute,
    ) {
        this.categories = APP_SETTINGS.categories;

        this.searchParams = this.router.url.toString().split('?')[1]
            ? urlToProperty('?' + this.router.url.toString().split('?')[1])
            : new ProductSearchParams('Asus', 2);

        this.search();
    }

    ngOnInit() {}

    search() {
        this.productService.getProducts(this.searchParams).subscribe(data => {

            this.router.navigate([], {
                relativeTo: this.activatedRoute,
                queryParams: this.searchParams,
                queryParamsHandling: 'merge',
            });

            let x: any = data;
            this.searchResultEmitter.emit(x);
            this.feedService.add(SEARCH_PRODUCT);
        });
    }
}
