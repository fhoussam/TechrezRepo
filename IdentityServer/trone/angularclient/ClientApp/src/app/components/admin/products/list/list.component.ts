import { Component, OnInit, Input } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { Router } from '@angular/router';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../../../../models/appState';
import { OPEN_PRODUCT } from '../../../../models/constants';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

    @Input() products: adminProductListItem[];
    selectedItemCode: string;
    constructor(
        private router: Router,
        private productEventEmitter: ProductEventEmitterService,
        private ngRedux: NgRedux<IAppState>
    ) { }

    ngOnInit() {
    }

    selectItem(selectedItem: adminProductListItem) {
        this.ngRedux.dispatch({ type : OPEN_PRODUCT });
        this.selectedItemCode = selectedItem.code;
        this.productEventEmitter.sendSelectedItem(selectedItem);
        let url: string = 'admin/products/explore/details/' + selectedItem.code;
        this.router.navigateByUrl(url);
    }
}
