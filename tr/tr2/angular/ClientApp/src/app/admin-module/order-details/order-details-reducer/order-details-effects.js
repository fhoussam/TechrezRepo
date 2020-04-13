"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var effects_1 = require("@ngrx/effects");
var order_details_actions_1 = require("./order-details-actions");
var operators_1 = require("rxjs/operators");
var OrderDetailsEffects = /** @class */ (function () {
    function OrderDetailsEffects(orderDetailsService, actions$) {
        var _this = this;
        this.orderDetailsService = orderDetailsService;
        this.actions$ = actions$;
        this.searchOrderDetails = this.actions$.pipe(effects_1.ofType(order_details_actions_1.SEARCH_ORDER_DETAILS_BEGIN), operators_1.switchMap(function (x) {
            return _this.orderDetailsService.searchOrderDetails(x.payload).pipe(operators_1.map(function (y) {
                var response = y;
                console.log(response);
                return new order_details_actions_1.SearchOrderDetailsEnd(response);
            }));
        }));
    }
    __decorate([
        effects_1.Effect()
    ], OrderDetailsEffects.prototype, "searchOrderDetails", void 0);
    OrderDetailsEffects = __decorate([
        core_1.Injectable()
    ], OrderDetailsEffects);
    return OrderDetailsEffects;
}());
exports.OrderDetailsEffects = OrderDetailsEffects;
//# sourceMappingURL=order-details-effects.js.map