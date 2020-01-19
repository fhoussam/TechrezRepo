import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { SAVE_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { adminProductEdit } from '../../../../models/adminProductEdit';
import { category } from '../../../../models/category';
import { APP_SETTINGS } from '../../../../models/APP_SETTINGS';
import { ProductService } from '../../../../services/product.service';
import { FeedService } from '../../../../services/feed.service';
import { Subscription, Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { CanCompoDeactivate } from '../../../../services/can-deactivate-guard-service';

@Component({
    selector: 'app-details',
    templateUrl: './editDetails.component.html',
    styleUrls: ['./editDetails.component.css']
})
export class EditDetailsComponent implements OnInit, OnDestroy, CanCompoDeactivate {

    categories: category[];
    selectedFile: File = null;
    routeSub: Subscription;
    itemDescription: string;
    @ViewChild('f', { static: false }) editFormRef: NgForm;

    CanDeactivate(): boolean | Observable<boolean> | Promise<boolean> {
        return this.editFormRef.dirty
            ? confirm('Do you want to discard the changes ?') : true;
    }

    ngOnDestroy(): void {
        this.routeSub.unsubscribe();
    }

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
            this.productService.getProduct(r[0].path).subscribe((x: adminProductEdit) => {
                this.editFormRef.reset();
                this.editFormRef.form.patchValue(x);
                this.itemDescription = x.description;
                
            });
        });
    }

    ngOnInit() {

    }

    saveChanges() {
        this.productService.save(this.editFormRef.form.value, this.selectedFile).subscribe((resp: any) => {
            //send changes to list compo
            this.productEventEmitter.sendSelectedItem(this.editFormRef.form.value as adminProductListItem);
            this.feedService.add(SAVE_PRODUCT);
        });
    }
}
