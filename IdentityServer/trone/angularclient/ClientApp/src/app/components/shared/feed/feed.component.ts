import { Component, OnInit } from '@angular/core';
import { select } from '@angular-redux/store';
import { Operation } from '../../../models/appState';
import { OPEN_PRODUCT, SEARCH_PRODUCT, SAVE_PRODUCT } from '../../../models/constants';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-feed',
    templateUrl: './feed.component.html',
    styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

    @select() operations;
    constructor(private datePipe: DatePipe) { }

    getOperationText(operation: Operation) {

        //console.log(operation.type);

        var action = 'has done something';
        switch (operation.type) {
            case OPEN_PRODUCT:
                action = 'has opened a product';
                break;
            case SEARCH_PRODUCT:
                action = 'has searched for a product';
                break;
            case SAVE_PRODUCT:
                action = 'has updated a product';
                break;
        }
        return operation.user + ' ' + action + ' at ' + this.datePipe.transform(operation.datetime, 'hh:mm:ss'); 
    }

    ngOnInit() {
    }

}
