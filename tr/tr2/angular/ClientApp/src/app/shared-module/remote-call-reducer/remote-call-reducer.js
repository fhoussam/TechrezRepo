"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var remote_call_actions_1 = require("./remote-call-actions");
var initialState = {
    messageType: remote_call_actions_1.SUCCESS,
    messageValue: null,
};
exports.appReducer = {
    remoteCallStatus: remoteCallStatusReducer,
};
function remoteCallStatusReducer(state, action) {
    if (state === void 0) { state = initialState; }
    try {
        return action.payload;
    }
    catch (e) {
        return initialState;
    }
}
exports.remoteCallStatusReducer = remoteCallStatusReducer;
//# sourceMappingURL=remote-call-reducer.js.map