import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Feed } from '../../../models/Feed';
import { FeedService } from '../../../services/feed.service';

@Component({
    selector: 'app-feed',
    templateUrl: './feed.component.html',
    styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

    feeds: Feed[];

    constructor(private datePipe: DatePipe, private feedService: FeedService) {
        feedService.getAll().subscribe((feeds) => {
            console.log(feeds);
            this.feeds = feeds; 
        });
    }

    getOperationText(feed: Feed) {
        var action = 'has done something';
        switch (feed.operationType) {
            case 'OPEN_PRODUCT':
                action = 'has opened a product';
                break;
            case 'SEARCH_PRODUCT':
                action = 'has searched for a product';
                break;
            case 'SAVE_PRODUCT':
                action = 'has updated a product';
                break;
        }
        return feed.userName + ' ' + action + ' at ' + this.datePipe.transform(feed.dateTimeStamp, 'hh:mm:ss'); 
    }

    ngOnInit() {
    }
}
