"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __spreadArrays = (this && this.__spreadArrays) || function () {
    for (var s = 0, i = 0, il = arguments.length; i < il; i++) s += arguments[i].length;
    for (var r = Array(s), k = 0, i = 0; i < il; i++)
        for (var a = arguments[i], j = 0, jl = a.length; j < jl; j++, k++)
            r[k] = a[j];
    return r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var FeedActions = require("./feeds.actions");
var initial_state = { feeds: [] };
function feedReducer(state, action) {
    if (state === void 0) { state = initial_state; }
    switch (action.type) {
        case FeedActions.ADD_FEED:
            return __assign(__assign({}, state), { feeds: __spreadArrays([action.payload], state.feeds) });
        case FeedActions.ADD_FEEDS:
            return __assign(__assign({}, state), { feeds: __spreadArrays(state.feeds, action.payload) });
        default:
            return state;
    }
}
exports.feedReducer = feedReducer;
//# sourceMappingURL=feed.reducer.js.map