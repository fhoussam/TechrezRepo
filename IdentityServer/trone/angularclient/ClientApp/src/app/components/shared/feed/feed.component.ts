import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Feed } from '../../../models/Feed';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { OPEN_PRODUCT, SEARCH_PRODUCT, SAVE_PRODUCT } from '../../../models/constants';

@Component({
    selector: 'app-feed',
    templateUrl: './feed.component.html',
    styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

    feeds: Observable<{ feeds: Feed[] }>;

    constructor(
        private datePipe: DatePipe,
        private feedStore: Store<{ feeds: { feeds: Feed[] }}>
    ) {
        this.feeds = this.feedStore.select('feeds');
    }

    getOperationText(feed: Feed) {
        let action : string = '';
        switch (feed.operationType) {
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
        return feed.userName + ' ' + action + ' at ' + this.datePipe.transform(feed.dateTimeStamp, 'hh:mm:ss'); 
    }

    ngOnInit() {
    }
}
