import { Component, OnInit, Input } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';
import { NgRedux } from '@angular-redux/store';
import { IAppState } from '../../../../models/appState';
import { OPEN_PRODUCT } from '../../../../models/constants';
import { propertyToUrl, urlToProperty, urlToList } from "query-string-params";
import { Location } from '@angular/common';

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
    };

    selectedItemCode: string;
    selectedItemPhotoUrl: string;

    constructor(
        private router: Router,
        private productEventEmitter: ProductEventEmitterService,
        private ngRedux: NgRedux<IAppState>,
        private location: Location,
    ) {

    }

    ngOnInit() {
    }

    selectItem(selectedItem: adminProductListItem) {

        this.ngRedux.dispatch({ type: OPEN_PRODUCT });

        this.selectedItemCode = selectedItem.code;
        this.selectedItemPhotoUrl = "http://localhost:5001/api/product/images/" + selectedItem.photoUrl;

        this.productEventEmitter.sendSelectedItem(selectedItem);

        let searchParams: any = urlToProperty(location.search);
        searchParams.si = selectedItem.code;
        let queryString: string = propertyToUrl(searchParams);

        let tab: string = "details";
        if (searchParams.st)
            tab = searchParams.st;

            //what a fkn headache !
        this.router.navigate(["/admin/products/" + tab], { queryParams: searchParams })
            .then(navResult => {
                //console.log('success : ' + navResult);
                let url: string = "/admin/products?" + queryString;
                this.location.replaceState(url);
            }).catch(navResult => {
                console.log("fail : " + navResult);
            });
    }
}
