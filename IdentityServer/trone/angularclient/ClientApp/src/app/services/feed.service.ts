import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { adminProductListItem } from '../models/adminProductListItem';
import { adminProductEdit } from '../models/adminProductEdit';
import { Feed } from '../models/appState';

@Injectable({
    providedIn: 'root'
})
export class FeedService {


    constructor(private http: HttpClient) { }
    url: string = 'http://localhost:5001/api/feed';

    getAll() {
        return this.http.get<adminProductListItem[]>(this.url);
    }

    add(feed: Feed) {
        return this.http.post(this.url, feed);
    }
}
