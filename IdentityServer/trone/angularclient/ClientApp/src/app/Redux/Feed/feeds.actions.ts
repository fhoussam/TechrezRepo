import { Action } from '@ngrx/store';
import { Feed } from '../../models/Feed';

export const ADD_FEED = 'ADD_FEED';
export const LOAD_FEEDS = 'LOAD_FEEDS';

export class AddFeed implements Action {
    readonly type = ADD_FEED;

    constructor(public payload: Feed) { }
}

export class LoadFeeds implements Action {
    readonly type = LOAD_FEEDS;

    constructor(public payload: Feed[]) { }
}

export type FeedAction = AddFeed | LoadFeeds;
