"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var remote_call_actions_1 = require("./remote-call-actions");
var initialState = {
    messageType: remote_call_actions_1.SUCCESS,
    messageValue: null,
};
function remoteCallStatusReducer(state, action) {
    if (state === void 0) { state = initialState; }
    if (action.type === "@ngrx/store/init") { //hacky, but it seems like "@ngrx/store/init" is actually the first action made by redux
        return initialState;
    }
    else
        return action.payload;
}
exports.remoteCallStatusReducer = remoteCallStatusReducer;
//# sourceMappingURL=remote-call-reducer.js.map