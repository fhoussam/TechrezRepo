import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { Feed } from '../models/Feed';
import { Store } from '@ngrx/store';
import { AppState } from '../Redux/Feed/feed.reducer';
import { AddFeed } from '../Redux/Feed/feeds.actions';

@Injectable({
    providedIn: 'root'
})
export class FeedService {

    constructor(
        private http: HttpClient,
        private feedStore: Store<AppState>,
    ) { }
    url: string = 'https://localhost:44301/api/feed';

    //to refactor
    getAll(lastSeen: string = null) {
        if (lastSeen) {
            let params = new HttpParams().set("lastSeen", lastSeen);
            return this.http.get<Feed[]>(this.url, { params: params });
        }
        else
            return this.http.get<Feed[]>(this.url);
    }

    add(operationType: string) {
        let feed: Feed = new Feed("Current User", new Date(), operationType);
        return this.http.post(this.url, feed).subscribe(() => this.feedStore.dispatch(new AddFeed(feed)));
    }
}
