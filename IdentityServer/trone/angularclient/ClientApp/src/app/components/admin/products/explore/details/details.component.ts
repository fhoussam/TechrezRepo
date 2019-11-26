import { Component, OnInit } from '@angular/core';
import { ExploreComponent } from '../explore.component';
import { adminProductEdit } from '../../../../../models/adminProductEdit';
import { ActivatedRoute } from '@angular/router';
import { adminProductListItem } from '../../../../../models/adminProductListItem';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

    itemToEdit: adminProductEdit;
    adminProductListItemPreviousState: adminProductListItem; 

    constructor(private host: ExploreComponent, private route: ActivatedRoute) {
        let id: string = this.route.snapshot.paramMap.get('id');
        this.adminProductListItemPreviousState = this.host.selectedItem;
    }

    ngOnInit() { }

    saveChanges() {
        this.host.selectedItem.price += 10;
        //this.host.selectedItem.price = this.itemToEdit.price;
        //this.host.selectedItem.description = this.itemToEdit.description;
        //this.host.selectedItem.quantity = this.itemToEdit.quantity;
    }
}
