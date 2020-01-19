import { Component, OnInit, Output, EventEmitter, OnDestroy, ViewChild } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { SEARCH_PRODUCT } from '../../../../models/constants';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductSearchParams } from '../../../../models/productSearchParam';
import { urlToProperty } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';

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
    searchErrorMessage: string;
    @ViewChild('f', {static: false}) searchForm: NgForm;

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

    ngOnInit() { }

    hasChanges(): boolean {
        for (let prop in this.searchForm.form.value) {
            if (this.searchForm.form.value[prop])
                return true;
        }

        return false;
    }

    search() {
        if (!this.hasChanges())
            this.searchErrorMessage = "Please provide at least one search filter";
        else {
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

    closeErrorDialog() {
        this.searchErrorMessage = null;
    }
}
