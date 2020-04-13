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
var order_details_actions_1 = require("./order-details-actions");
var OrderDetailsStateInitalState = {
    list: null,
    selectedItem: null,
};
function OrderDetailsReducer(state, action) {
    if (state === void 0) { state = OrderDetailsStateInitalState; }
    switch (action.type) {
        case order_details_actions_1.SEARCH_ORDER_DETAILS_END:
            return __assign(__assign({}, state), { list: action.payload });
        default:
            state;
    }
}
exports.OrderDetailsReducer = OrderDetailsReducer;
//# sourceMappingURL=order-details-reducer.js.map