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
var OrderDetailsService = /** @class */ (function () {
    function OrderDetailsService(http, httpHelper) {
        this.http = http;
        this.httpHelper = httpHelper;
        this.baseUrl = '/api/orderDetails';
    }
    OrderDetailsService.prototype.searchOrderDetails = function (searchOrderDetailsQuery) {
        return this.http.get('/api/orderDetails?', { params: this.httpHelper.toHttpParams(searchOrderDetailsQuery) });
    };
    OrderDetailsService.prototype.getOrderDetails = function (orderId, productId, forEdit) {
        var url = this.baseUrl + '/' + productId + '/' + orderId + '?isEdit=' + forEdit;
        return this.http.get(url);
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