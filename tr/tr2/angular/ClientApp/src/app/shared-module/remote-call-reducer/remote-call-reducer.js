"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var remote_call_actions_1 = require("./remote-call-actions");
var RemoteCallStatus = /** @class */ (function () {
    function RemoteCallStatus() {
    }
    return RemoteCallStatus;
}());
exports.RemoteCallStatus = RemoteCallStatus;
var initialState = {
    messageType: remote_call_actions_1.SUCCESS,
    messageValue: null,
};
function remoteCallStatusReducer(state, action) {
    if (state === void 0) { state = initialState; }
    try {
        var data = action.payload;
        return {
            messageType: data.messageType,
            messageValue: data.messageValue,
        };
    }
    catch (e) {
        return state;
    }
}
exports.remoteCallStatusReducer = remoteCallStatusReducer;
//# sourceMappingURL=remote-call-reducer.js.map