import { Action } from '@ngrx/store';
import { Feed } from '../../models/Feed';

export const ADD_FEED = 'ADD_FEED';
export const ADD_FEEDS = 'ADD_FEEDS';

export class AddFeed implements Action {
    readonly type = ADD_FEED;

    constructor(public payload: Feed) { }
}

export class AddFeeds implements Action {
    readonly type = ADD_FEEDS;

    constructor(public payload: Feed[]) { }
}

export type FeedAction = AddFeed | AddFeeds;
