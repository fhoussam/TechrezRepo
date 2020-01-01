import { Action } from '@ngrx/store';
import { Feed } from '../../models/Feed';

export const ADD_FEED = 'ADD_FEED';
export const ADD_OLD_FEEDS = 'ADD_OLD_FEEDS';
export const ADD_NEW_FEEDS = 'ADD_NEW_FEEDS';

export class AddFeed implements Action {
    readonly type = ADD_FEED;

    constructor(public payload: Feed) { }
}

export class AddOldFeeds implements Action {
    readonly type = ADD_OLD_FEEDS;

    constructor(public payload: Feed[]) { }
}

export class AddNewFeeds implements Action {
    readonly type = ADD_NEW_FEEDS;

    constructor(public payload: Feed[]) { }
}

export type FeedAction = AddFeed | AddOldFeeds | AddNewFeeds;
