import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { OPEN_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';
import { ProductSearchParams } from '../../../../models/productSearchParam';
import { Subscription } from 'rxjs';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy {

    @Input() products: adminProductListItem[];
    selectedItem: adminProductListItem;
    routeSub: Subscription;

    constructor(
        private router: Router,
        private productEventEmitter: ProductEventEmitterService, //just to send potential changes back
        private feedService: FeedService,
        private activatedRoute: ActivatedRoute,
    ) {

    }

    ngOnInit() {
        if (this.router.url.toString().split('?')[1]) {
            let productid = this.router.url.match(/\d{2}/);
            if (productid && this.products) {
                this.selectItem(this.products.find((x) => x.code == productid.toString()));
            }
        }

        //sub, destroy, pipe async for visiblity ofsub tabs, get rid of regexp in selectitem method
        //next step : http://localhost:4200/admin/products/22
        this.routeSub = this.activatedRoute.firstChild.url.subscribe(x => {
            if (x[0])
              console.log(x[0].path);
        });
    }

    ngOnDestroy(): void {
        this.routeSub.unsubscribe();
    }

    selectItem(selectedItem: adminProductListItem) {

        this.selectedItem = selectedItem;

        if (this.router.url.split('/').length == 5) {
            let previous_productid = this.router.url.match(/\d{2}/);
            let newUrl: string = this.router.url
                .replace('/' + previous_productid + '/', '/' + selectedItem.code + '/');
            this.router.navigateByUrl(newUrl, {
                queryParamsHandling: "merge",
            }).then(() => {
                this.feedService.add(OPEN_PRODUCT);
            });
        }
        else {
            this.productEventEmitter.sendSelectedItem(selectedItem);
            this.router.navigate(
                [selectedItem.code, this.router.url.split('/')[4] || 'details'],
                {
                    relativeTo: this.activatedRoute,
                    queryParamsHandling: "merge",
                }
            ).then(() => {
                this.feedService.add(OPEN_PRODUCT);
            });
        }
    }
}
