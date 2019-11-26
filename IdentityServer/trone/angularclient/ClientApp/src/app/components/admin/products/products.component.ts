import { Component, OnInit, Input } from '@angular/core';
import { adminProductListItem } from '../../../models/adminProductListItem';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

    constructor() { }
    searchResult: adminProductListItem[] = [];
    selectedItem: adminProductListItem;

    ngOnInit() {
    }

    displaySearchResult(searchResult) {
        this.searchResult = searchResult;
    }

    sendSelectedItemToExplore(selectedItem) {
        this.selectedItem = selectedItem;
    }
}
