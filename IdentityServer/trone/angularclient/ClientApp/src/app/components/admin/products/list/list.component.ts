import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { Router } from '@angular/router';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

    @Input() products: adminProductListItem[];
    @Output() itemSelectedEvent = new EventEmitter();
    selectedItem: adminProductListItem;
    constructor(private router: Router) {}

    ngOnInit() {
    }

    selectItem(selectedItem) {
        this.selectedItem = selectedItem;
        this.itemSelectedEvent.emit(selectedItem);
        let url: string = 'admin/products/explore/details/' + this.selectedItem.code;
        this.router.navigateByUrl(url);
    }
}
