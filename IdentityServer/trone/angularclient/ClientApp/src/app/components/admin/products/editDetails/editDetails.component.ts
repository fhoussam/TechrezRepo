import { Component, OnInit, OnDestroy } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { SAVE_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { adminProductEdit } from '../../../../models/adminProductEdit';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductService } from '../../../../services/product.service';
import { FeedService } from '../../../../services/feed.service';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-details',
    templateUrl: './editDetails.component.html',
    styleUrls: ['./editDetails.component.css']
})
export class EditDetailsComponent implements OnInit, OnDestroy {

    ngOnDestroy(): void {
        this.routeSub.unsubscribe();
    }

    product: adminProductEdit;
    categories: category[];
    selectedFile: File = null;
    routeSub: Subscription;
    itemDescription: string;

    fileSelected(event) {
        this.selectedFile = <File>event.target.files[0];
    }

    constructor(
        private productService: ProductService,
        private feedService: FeedService,
        private router: Router,
        private route: ActivatedRoute,
        private productEventEmitter: ProductEventEmitterService,
    ) {
        this.categories = APP_SETTINGS.categories;
       
        this.routeSub = this.route.url.subscribe((r) => {
            this.productService.getProduct(r[0].path).subscribe((x:adminProductEdit) => {
                this.product = new adminProductEdit();
                this.product.code = x.code;
                this.product.categoryId = x.categoryId;
                this.product.description = x.description;
                this.product.price = x.price;
                this.product.quantity = x.quantity;
                this.itemDescription = x.description;
            });
        });

        
    }

    ngOnInit() {

    }

    saveChanges() {
        this.productService.save(this.product, this.selectedFile).subscribe((resp: any) => {
            //send changes to list compo
            this.productEventEmitter.sendSelectedItem(this.product.toAdminProductListItem());
            this.feedService.add(SAVE_PRODUCT);
        });
    }
}
