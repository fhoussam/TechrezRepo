"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.INITIAL_STATE = {
    operations: [],
};
function rootReducer(state, action) {
    action.feed = new Feed();
    action.feed.userName = 'Current User';
    action.feed.dateTimeStamp = new Date();
    action.feed.operationType = action.type;
    var result = Object.assign({}, state, {
        operations: state.operations
            .concat(Object.assign({}, action.feed))
            .sort(function (a, b) {
            return b.dateTimeStamp.getTime() - a.dateTimeStamp.getTime();
        })
    });
    return result;
}
exports.rootReducer = rootReducer;
var Feed = /** @class */ (function () {
    function Feed() {
        this.userName = '';
        this.dateTimeStamp = null;
        this.operationType = null;
    }
    return Feed;
}());
exports.Feed = Feed;
//# sourceMappingURL=appState.js.map