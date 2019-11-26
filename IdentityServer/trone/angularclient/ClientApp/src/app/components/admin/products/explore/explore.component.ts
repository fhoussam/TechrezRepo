import { Component, OnInit } from '@angular/core';
import { ProductEventEmitterService } from '../../../../services/product-event-emitter.service';

@Component({
    selector: 'explore',
    templateUrl: './explore.component.html',
    styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {

    selectedItemCode: string;
    constructor(private productEventEmitter: ProductEventEmitterService) {
        this.productEventEmitter.cast.subscribe(selectedItem => this.selectedItemCode = selectedItem.code);
    }
    ngOnInit() {}
}
