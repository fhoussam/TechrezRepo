"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var APP_SETTINGS_1 = require("../shared-module/models/APP_SETTINGS");
var OrderDetailsService = /** @class */ (function () {
    function OrderDetailsService(http, httpHelper, datepipe) {
        this.http = http;
        this.httpHelper = httpHelper;
        this.datepipe = datepipe;
    }
    OrderDetailsService.prototype.searchOrderDetails = function (searchOrderDetailsQuery) {
        var Params = new http_1.HttpParams();
        Params = Params.append('orderDateFrom', this.datepipe.transform(searchOrderDetailsQuery.orderDateFrom, APP_SETTINGS_1.APP_SETTINGS.queryStringDateFormat));
        Params = Params.append('orderDateTo', this.datepipe.transform(searchOrderDetailsQuery.orderDateTo, APP_SETTINGS_1.APP_SETTINGS.queryStringDateFormat));
        Params = Params.append('productId', searchOrderDetailsQuery.productId.toString());
        return this.http.get('/api/orderDetails?', { params: Params });
    };
    OrderDetailsService.prototype.getOrderDetails = function (orderId, productId, forEdit) {
        var params = new http_1.HttpParams();
        params.append('orderId', orderId.toString());
        params.append('productId', productId.toString());
        params.append('forEdit', forEdit.toString());
        return this.http.get(this.baseUrl, { params: params });
    };
    OrderDetailsService.prototype.deleteOrderDetails = function (orderId, productId) {
        var params = new http_1.HttpParams();
        params.append('orderId', orderId.toString());
        params.append('productId', productId.toString());
        return this.http.delete(this.baseUrl, { params: params });
    };
    OrderDetailsService.prototype.editOrderDetails = function (editOrderDetailCommand) {
        return this.http.post(this.baseUrl, editOrderDetailCommand);
    };
    OrderDetailsService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], OrderDetailsService);
    return OrderDetailsService;
}());
exports.OrderDetailsService = OrderDetailsService;
//# sourceMappingURL=order-details.service.js.map