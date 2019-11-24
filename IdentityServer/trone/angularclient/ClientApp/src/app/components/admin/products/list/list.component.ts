import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '../../../../services/product.service';
import { adminProductListItem } from '../../../../models/adminProductListItem';

@Component({
    selector: 'list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

    @Input() products: adminProductListItem[];
    constructor() {}

    selectItem() {

    }

    ngOnInit() {
    }

}
