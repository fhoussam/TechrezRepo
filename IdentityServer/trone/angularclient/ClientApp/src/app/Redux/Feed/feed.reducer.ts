import { Feed } from '../../models/Feed';
import * as FeedActions from './feeds.actions';

export interface AppState {
    feeds: Feed[];
}

const initial_state = { feeds: [] }

export function feedReducer(state = initial_state, action: FeedActions.FeedAction) {
    switch (action.type) {
        case FeedActions.ADD_FEED:
            return {
                ...state,
                feeds: [action.payload, ...state.feeds]
            };
        case FeedActions.ADD_OLD_FEEDS:
            return {
                ...state,
                feeds: [...state.feeds, ...action.payload]
            };
        case FeedActions.ADD_NEW_FEEDS:
            return {
                ...state,
                feeds: [...action.payload, ...state.feeds]
            };
        default:
            return state;
    }
}
