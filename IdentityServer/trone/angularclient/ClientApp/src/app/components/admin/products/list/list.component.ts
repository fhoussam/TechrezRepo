import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { Router, ActivatedRoute, UrlSegment, NavigationEnd } from '@angular/router';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { OPEN_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';
import { ProductSearchParams } from '../../../../models/productSearchParam';
import { Subscription, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy {

    @Input() products: adminProductListItem[];
    selectedItem: adminProductListItem;
    currentItemId: string;
    routeSub: Subscription;

    constructor(
        private router: Router,
        private feedService: FeedService,
        private route: ActivatedRoute,
        private productEventEmitter: ProductEventEmitterService,
    ) {
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

        //because subscribtion below does not fire when user tries direct access toi an item
        if (this.route.firstChild) {
            this.currentItemId = this.route.firstChild.snapshot.url[0].toString();
        }

        this.routeSub = this.router.events.subscribe((event: any) => {
                if (event instanceof NavigationEnd) {
                    this.currentItemId = event.url.split('/')[3];
                }
            }
        );
    }

    ngOnDestroy(): void {
        this.routeSub.unsubscribe();
    }

    selectItem(selectedItem: adminProductListItem) {
        let selectedTab = this.route.snapshot.firstChild
            ? this.route.snapshot.firstChild.routeConfig.path.split('/')[1]
            : 'details';

        this.router.navigate(
            [selectedItem.code + '/' + selectedTab],
            { relativeTo: this.route, queryParamsHandling: "merge" }
        ).then((navigationSucceded) => {
            if (navigationSucceded) {
                this.selectedItem = selectedItem;
                this.currentItemId = selectedItem.code;
                this.feedService.add(OPEN_PRODUCT);
            }
        });
    }
}
