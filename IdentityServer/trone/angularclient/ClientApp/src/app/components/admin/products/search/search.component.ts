import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { adminProductListItem } from '../../../../models/adminProductListItem';
import { ProductService } from '../../../../services/product.service';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    @Output() searchResultEmitter = new EventEmitter();
    constructor(private productService: ProductService) {
        this.search();
    }
    ngOnInit() {}

    search() {
        this.productService.getProducts().subscribe(data => {
            let x: any = data;
            this.searchResultEmitter.emit(x);
        });
    }
}
