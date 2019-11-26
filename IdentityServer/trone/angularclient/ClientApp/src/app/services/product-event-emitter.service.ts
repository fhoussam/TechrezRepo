import { Injectable } from '@angular/core';
import { adminProductListItem } from '../models/adminProductListItem';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ProductEventEmitterService {

    private selectedItem = new BehaviorSubject<adminProductListItem>(new adminProductListItem());
    cast = this.selectedItem.asObservable();
    constructor() { }
    sendSelectedItem(selectedItem: adminProductListItem) {
        this.selectedItem.next(selectedItem);
    }
}
