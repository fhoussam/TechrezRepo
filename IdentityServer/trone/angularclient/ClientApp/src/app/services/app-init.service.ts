import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { category } from '../models/category';
import { APP_SETTINGS } from '../models/APP_SETTINGS';
import { Store } from '@ngrx/store';
import { FeedService } from './feed.service';
import { AppState } from '../Redux/Feed/feed.reducer';
import { LoadFeeds } from '../Redux/Feed/feeds.actions';

@Injectable({
  providedIn: 'root'
})
export class AppInitService {

    constructor(
        private httpClient: HttpClient,
        private feedStore: Store<AppState>,
        private feedService: FeedService,
    ) { }

    getSettings(): Promise<any> {

        const promise_categories = this.httpClient.get<category[]>('http://localhost:5001/api/product/categories').toPromise()
            .then(settings => APP_SETTINGS.categories = settings);

        const promise_inital_feeds = this.feedService.getAll().toPromise()
            .then(feeds => this.feedStore.dispatch(new LoadFeeds(feeds)));

        return Promise.all([promise_categories, promise_inital_feeds]);
    }
}
