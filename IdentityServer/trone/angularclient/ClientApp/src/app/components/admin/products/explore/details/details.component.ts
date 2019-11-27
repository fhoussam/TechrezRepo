import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { adminProductListItem } from '../../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../../services/product-event-emitter.service';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../../../../../models/appState';
import { SAVE_PRODUCT } from '../../../../../models/constants';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

    selectedItem: adminProductListItem;
    constructor(
        private route: ActivatedRoute,
        private productEventEmitter: ProductEventEmitterService,
        private ngRedux: NgRedux<IAppState>,
    ) {
        let id: string = this.route.snapshot.paramMap.get('id');
        this.productEventEmitter.cast.subscribe(selectedItem => this.selectedItem = selectedItem);
    }

    ngOnInit() { }

    saveChanges() {
        this.selectedItem.price += 10;
        this.ngRedux.dispatch({ type: SAVE_PRODUCT });
    }
}
