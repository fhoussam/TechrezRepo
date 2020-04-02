"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var spiner_actions_1 = require("./spiner-actions");
var initialState = {
    messageType: spiner_actions_1.SUCCESS,
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
//# sourceMappingURL=spiner-reducer.js.map