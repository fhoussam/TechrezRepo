import { SEARCH_PRODUCT, SAVE_PRODUCT, OPEN_PRODUCT } from './constants';

export interface IAppState {
    operations: Feed[];
}

export const INITIAL_STATE: IAppState = {
    operations : [],
}

export function rootReducer(state, action) {

    action.feed = new Feed();
    action.feed.userName = 'Current User';
    action.feed.dateTimeStamp = new Date();
    action.feed.operationType = action.type;

    var result = (<any>Object).assign({}, state, {
        operations: state.operations
            .concat((<any>Object).assign({}, action.feed))
            .sort((a: Feed, b: Feed) => {
                return b.dateTimeStamp.getTime() - a.dateTimeStamp.getTime();
            })
    });

    return result;

}

export class Feed {
    userName: string;
    dateTimeStamp: Date;
    operationType: string;

    constructor() {
        this.userName = '';
        this.dateTimeStamp = null;
        this.operationType = null;
    }
}
