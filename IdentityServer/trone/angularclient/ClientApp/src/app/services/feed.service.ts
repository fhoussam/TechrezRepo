import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Feed } from '../models/Feed';
import { Store } from '@ngrx/store';
import * as FeedActions from '../Redux/Feed/feeds.actions';

@Injectable({
    providedIn: 'root'
})
export class FeedService {

    constructor(
        private http: HttpClient,
        private feedStore: Store<{ feeds: { feeds: Feed[] } }>,
    ) { }
    url: string = 'http://localhost:5001/api/feed';

    getAll() {
        return this.http.get<Feed[]>(this.url);
    }

    add(operationType: string) {
        let feed: Feed = new Feed("Current User", new Date(), operationType);
        return this.http.post(this.url, feed).subscribe(() => this.feedStore.dispatch(new FeedActions.AddFeed(feed)));
    }
}
