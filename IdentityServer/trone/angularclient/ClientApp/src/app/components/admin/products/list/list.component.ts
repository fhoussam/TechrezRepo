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
    currentId: string;

    constructor(
        private router: Router,
        private feedService: FeedService,
        private route: ActivatedRoute,
        private productEventEmitter: ProductEventEmitterService,
    ) {
        //list compo is loaded twice, to be fixed
        //console.log('!!!!!!');
    }

    ngOnInit() {
        this.productEventEmitter.cast.subscribe(receivedItem => {
            if (this.products) {
                let itemToUpdate = this.products.find((x) => x.code == receivedItem.code);
                if (itemToUpdate) {
                    itemToUpdate.categoryId = receivedItem.categoryId;
                    itemToUpdate.description = receivedItem.description;
                    itemToUpdate.price = receivedItem.price;
                    itemToUpdate.quantity = receivedItem.quantity;
                    if (!!receivedItem.photoUrl)
                        itemToUpdate.photoUrl = receivedItem.photoUrl;
                }
            }
        });

        if (this.route.firstChild) {
            this.routeSub = this.route.firstChild.url.subscribe((x) => {
                if (!!x[0] && !!x[0].path) {
                    this.currentId = x[0].path;
                }
            });
        }
    }

    ngOnDestroy(): void {
        if (this.routeSub)
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
            this.router.navigate(
                [selectedItem.code, this.router.url.split('/')[4] || 'details'],
                {
                    relativeTo: this.route,
                    queryParamsHandling: "merge",
                }
            ).then(() => {
                this.feedService.add(OPEN_PRODUCT);
            });
        }
    }
}
