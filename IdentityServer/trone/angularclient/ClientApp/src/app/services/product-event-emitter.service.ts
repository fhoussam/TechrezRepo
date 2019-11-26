import { Injectable } from '@angular/core';
import { EventEmitter } from 'events';
import { adminProductListItem } from '../models/adminProductListItem';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ProductEventEmitterService {

    private user = new BehaviorSubject<string>('john');
    cast = this.user.asObservable();
    constructor() { }
    sendSelectedItem(newUser) {
        this.user.next(newUser);
    }
}
