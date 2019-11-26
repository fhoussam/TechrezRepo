import { Component, OnInit } from '@angular/core';
import { ExploreComponent } from '../explore.component';
import { adminProductEdit } from '../../../../../models/adminProductEdit';
import { ActivatedRoute } from '@angular/router';
import { adminProductListItem } from '../../../../../models/adminProductListItem';
import { ProductEventEmitterService } from '../../../../../services/product-event-emitter.service';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

    itemToEdit: adminProductEdit;
    adminProductListItemPreviousState: adminProductListItem;
    userReflection: string;

    constructor(private host: ExploreComponent, private route: ActivatedRoute
        , private productEventEmitter: ProductEventEmitterService) {
        let id: string = this.route.snapshot.paramMap.get('id');
        //this.adminProductListItemPreviousState = this.host.selectedItem;
        //this.productEventEmitter.event.s
        this.productEventEmitter.cast.subscribe(user => this.userReflection = user);
    }

    ngOnInit() { }

    saveChanges() {
        this.host.selectedItem.price += 10;
        //this.host.selectedItem.price = this.itemToEdit.price;
        //this.host.selectedItem.description = this.itemToEdit.description;
        //this.host.selectedItem.quantity = this.itemToEdit.quantity;
    }
}
