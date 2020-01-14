import { Component, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { SEARCH_PRODUCT } from '../../../../models/constants';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductSearchParams } from '../../../../models/productSearchParam';
import { urlToProperty } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {

    ngOnDestroy(): void {
        this.routeSub.unsubscribe();
    }

    @Output() searchResultEmitter = new EventEmitter();
    categoryId: string;
    categories = APP_SETTINGS.categories;
    searchParams = new ProductSearchParams();;
    routeSub: Subscription;

    constructor(
        private productService: ProductService,
        private feedService: FeedService,
        private router: Router,
        private route: ActivatedRoute,
    ) {
        this.routeSub = this.route.queryParams.subscribe((x: ProductSearchParams) => {
            this.searchParams.categoryId = x.categoryId;
            this.searchParams.description = x.description;
        });
    }

    ngOnInit() {}

    search() {
        this.productService.getProducts(this.searchParams).subscribe(data => {

            this.router.navigate([], {
                relativeTo: this.route,
                queryParams: this.searchParams,
                queryParamsHandling: 'merge',
            });

            let x: any = data;
            this.searchResultEmitter.emit(x);
            this.feedService.add(SEARCH_PRODUCT);
        });
    }
}
