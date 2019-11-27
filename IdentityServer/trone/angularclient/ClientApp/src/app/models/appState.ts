import { SEARCH_PRODUCT, SAVE_PRODUCT, OPEN_PRODUCT } from './constants';


export interface IAppState {
    lastOperationTimeStamp: Date;
}

export const INITIAL_STATE: IAppState = {
    lastOperationTimeStamp: null,
}

export function rootReducer(state, action) {
    switch (action.type) {
        case SEARCH_PRODUCT:
            return (<any>Object).assign({}, state, {
                lastOperationTimeStamp: new Date()
            });
        case SAVE_PRODUCT:
            return (<any>Object).assign({}, state, {
                lastOperationTimeStamp: new Date()
            });
        case OPEN_PRODUCT:
            return (<any>Object).assign({}, state, {
                lastOperationTimeStamp: new Date()
            });
    }
}
