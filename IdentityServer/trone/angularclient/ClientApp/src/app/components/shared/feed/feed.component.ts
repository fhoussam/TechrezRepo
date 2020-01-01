import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Feed } from '../../../models/Feed';
import { Store } from '@ngrx/store';
import { Observable, BehaviorSubject } from 'rxjs';
import { OPEN_PRODUCT, SEARCH_PRODUCT, SAVE_PRODUCT } from '../../../models/constants';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { throttleTime, mergeMap, scan, map, tap } from 'rxjs/operators';
import { FeedService } from '../../../services/feed.service';
import { AddOldFeeds } from '../../../Redux/Feed/feeds.actions';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from '../../../services/signalr.service';

@Component({
    selector: 'app-feed',
    templateUrl: './feed.component.html',
    styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

    feeds: Feed[] = [];
    @ViewChild(CdkVirtualScrollViewport, { static: false }) viewport: CdkVirtualScrollViewport;
    theEnd: boolean = false;
    offset = new BehaviorSubject(null);

    constructor(
        private datePipe: DatePipe,
        private feedService: FeedService,
        private feedStore: Store<{ feeds: { feeds: Feed[] } }>,
        public signalRService: SignalRService,
        private http: HttpClient
    ) {
        this.feedStore.select('feeds').subscribe(a => {
            this.feeds = a.feeds;
            //console.log(this.feeds.length);
        });

        this.offset.pipe(
            throttleTime(1000),
            mergeMap(x => this.getBatch(x)),
            map(x => Object.values(x)),
            tap(x => this.theEnd = x.length == 0),
        ).subscribe((x: Feed[]) => {
            this.feedStore.dispatch(new AddOldFeeds(x))
        });
    }

    getBatch(lastSeen: any): any {
        return this.feedService.getAll(lastSeen);
    }

    nextBatch(event, offset) {
        if (this.theEnd)
            return;

        const end = this.viewport.getRenderedRange().end;
        const total = this.viewport.getDataLength();

        if (end == total) {
            this.offset.next(offset);
        }
    }

    getOperationText(feed: Feed) {
        let action: string = '';
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
        this.signalRService.startConnection();
        this.signalRService.addTransferFeedDataListener();
        //this.signalRService.addBroadcastFeedDataListener();
        //this.startHttpRequest();
    }

    //private startHttpRequest = () => {
    //    this.http.get('http://localhost:5001/api/feed/triggerFakeOperation')
    //        .subscribe(res => {
    //            console.log(res);
    //        })
    //}
}
