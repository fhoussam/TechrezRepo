import { SEARCH_PRODUCT, SAVE_PRODUCT, OPEN_PRODUCT } from './constants';

export interface IAppState {
    operations: Operation[];
}

export const INITIAL_STATE: IAppState = {
    operations : [],
}

export function rootReducer(state, action) {

    action.operation = new Operation();
    action.operation.user = 'Current User';
    action.operation.datetime = new Date();
    action.operation.type = action.type;

    var result = (<any>Object).assign({}, state, {
        operations: state.operations
            .concat((<any>Object).assign({}, action.operation))
            .sort((a: Operation, b: Operation) => {
                return b.datetime.getTime() - a.datetime.getTime();
            })
    });

    return result;

}

export class Operation {
    user: string;
    datetime: Date;
    type: string;

    constructor() {
        this.user = '';
        this.datetime = null;
        this.type = null;
    }
}
