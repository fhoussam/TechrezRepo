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
Object.defineProperty(exports, "__esModule", { value: true });
var app_init_actions_1 = require("./app-init-actions");
var appInitInitalState = {
    categories: [],
    antiforgery: false,
};
function appInitReducer(state, action) {
    if (state === void 0) { state = appInitInitalState; }
    switch (action.type) {
        case app_init_actions_1.INIT_ANTIFORGERY_END:
            console.log('app settings - antiforgery initialized ');
            return __assign(__assign({}, state), { antiforgery: true });
        case app_init_actions_1.INIT_CATEGORIES_END:
            console.log('app settings - categories initialized ', action.payload);
            return __assign(__assign({}, state), { categories: action.payload });
        default:
            return state;
    }
}
exports.appInitReducer = appInitReducer;
//# sourceMappingURL=app-init-reducer.js.map