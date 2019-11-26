import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { adminProductListItem } from '../../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../../services/product-event-emitter.service';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

    selectedItem: adminProductListItem;
    constructor(private route: ActivatedRoute, private productEventEmitter: ProductEventEmitterService) {
        let id: string = this.route.snapshot.paramMap.get('id');
        this.productEventEmitter.cast.subscribe(selectedItem => this.selectedItem = selectedItem);
    }

    ngOnInit() { }

    saveChanges() {
        this.selectedItem.price += 10;
    }
}
