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
var spiner_actions_1 = require("./spiner-actions");
var spiner_actions_2 = require("./spiner-actions");
var initialState = {
    messageType: spiner_actions_1.SUCCESS,
    messageValue: null,
    yesAsyncCallback: null,
};
function remoteCallStatusReducer(state, action) {
    if (state === void 0) { state = initialState; }
    switch (action.type) {
        case spiner_actions_2.ERROR:
            return __assign(__assign({}, state), { messageType: spiner_actions_1.SUCCESS, messageValue: action.payload });
        case spiner_actions_1.ALERT:
            return __assign(__assign({}, state), { messageType: spiner_actions_1.ALERT, messageValue: action.payload });
        case spiner_actions_1.PENDING:
            return __assign(__assign({}, state), { messageType: spiner_actions_1.PENDING, messageValue: action.payload });
        case spiner_actions_1.SUCCESS:
            return __assign(__assign({}, state), { messageType: spiner_actions_1.SUCCESS, messageValue: null, yesAsyncCallback: null });
        case spiner_actions_1.CONFIRM: {
            var confirmPayload = action.payload;
            return __assign(__assign({}, state), { messageType: spiner_actions_1.CONFIRM, messageValue: confirmPayload.messageValue, yesAsyncCallback: confirmPayload.yesAsyncCallback });
        }
        default:
            return initialState;
    }
}
exports.remoteCallStatusReducer = remoteCallStatusReducer;
//# sourceMappingURL=spiner-reducer.js.map