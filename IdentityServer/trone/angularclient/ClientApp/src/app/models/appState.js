"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.INITIAL_STATE = {
    operations: [],
};
function rootReducer(state, action) {
    action.operation = new Operation();
    action.operation.user = 'Current User';
    action.operation.datetime = new Date();
    action.operation.type = action.type;
    var result = Object.assign({}, state, {
        operations: state.operations
            .concat(Object.assign({}, action.operation))
            .sort(function (a, b) {
            return b.datetime.getTime() - a.datetime.getTime();
        })
    });
    return result;
}
exports.rootReducer = rootReducer;
var Operation = /** @class */ (function () {
    function Operation() {
        this.user = '';
        this.datetime = null;
        this.type = null;
    }
    return Operation;
}());
exports.Operation = Operation;
//# sourceMappingURL=appState.js.map