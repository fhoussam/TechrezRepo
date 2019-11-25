import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { adminProductListItem } from '../../../../models/adminProductListItem';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

    @Input() products: adminProductListItem[];
    @Output() itemSelectedEvent = new EventEmitter();
    constructor() {}

    selectItem(selectedItem) {
        let x: any = selectedItem;
        this.itemSelectedEvent.emit(x);
    }

    ngOnInit() {
    }

}
