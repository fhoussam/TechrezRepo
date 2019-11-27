"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var constants_1 = require("./constants");
exports.INITIAL_STATE = {
    lastOperationTimeStamp: null,
};
function rootReducer(state, action) {
    switch (action.type) {
        case constants_1.SEARCH_PRODUCT:
            return Object.assign({}, state, {
                lastOperationTimeStamp: new Date()
            });
        case constants_1.SAVE_PRODUCT:
            return Object.assign({}, state, {
                lastOperationTimeStamp: new Date()
            });
        case constants_1.OPEN_PRODUCT:
            return Object.assign({}, state, {
                lastOperationTimeStamp: new Date()
            });
    }
}
exports.rootReducer = rootReducer;
//# sourceMappingURL=appState.js.map