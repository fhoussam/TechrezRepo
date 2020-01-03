import { Component, OnInit, Input } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { OPEN_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { FeedService } from '../../../../services/feed.service';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

    //we had to use setter prop to know when the list of products is filled
    //either we use getter/setter strategy or on ngOnChanges from angular/core
    //warnig, setter get triggerted 2 times (for some reason)
    private _products;
    get products(): adminProductListItem[] {

        return this._products;
    }
    @Input()
    set products(val: adminProductListItem[]) {
        this._products = val;

        if (val.length > 0 && location.search) {
            let searchParams: any = urlToProperty(location.search);
            let itemToSelect: adminProductListItem = val.find(x => x.code == searchParams.si);
            if (itemToSelect)
                this.selectItem(itemToSelect);
        }
    }

    selectedItem: adminProductListItem;

    constructor(
        private router: Router,
        private productEventEmitter: ProductEventEmitterService,
        private feedService: FeedService,
    ) {

    }

    ngOnInit() {
    }

    selectItem(selectedItem: adminProductListItem) {
        this.selectedItem = selectedItem;
        this.productEventEmitter.sendSelectedItem(selectedItem);
        let searchParams: any = urlToProperty(location.search);
        searchParams.si = selectedItem.code;
        let queryString: string = propertyToUrl(searchParams);
        var st = searchParams.st != null ? searchParams.st : "details";
        this.router.navigateByUrl("/admin/products/" + st + "?" + queryString).then(() => {
            this.feedService.add(OPEN_PRODUCT);
        });
    }
}
