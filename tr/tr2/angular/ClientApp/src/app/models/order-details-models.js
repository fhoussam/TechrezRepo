"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var Pager_1 = require("./Pager");
var SearchOrderDetailsQuery = /** @class */ (function (_super) {
    __extends(SearchOrderDetailsQuery, _super);
    function SearchOrderDetailsQuery() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return SearchOrderDetailsQuery;
}(Pager_1.Pager));
exports.SearchOrderDetailsQuery = SearchOrderDetailsQuery;
var GetOrderDetailForDisplayResponse = /** @class */ (function () {
    function GetOrderDetailForDisplayResponse() {
    }
    return GetOrderDetailForDisplayResponse;
}());
exports.GetOrderDetailForDisplayResponse = GetOrderDetailForDisplayResponse;
var GetOrderDetailsForEditResponse = /** @class */ (function () {
    function GetOrderDetailsForEditResponse() {
    }
    return GetOrderDetailsForEditResponse;
}());
exports.GetOrderDetailsForEditResponse = GetOrderDetailsForEditResponse;
var SearchOrderDetailsResponse = /** @class */ (function () {
    function SearchOrderDetailsResponse() {
    }
    return SearchOrderDetailsResponse;
}());
exports.SearchOrderDetailsResponse = SearchOrderDetailsResponse;
var EditOrderDetailCommand = /** @class */ (function () {
    function EditOrderDetailCommand() {
    }
    return EditOrderDetailCommand;
}());
exports.EditOrderDetailCommand = EditOrderDetailCommand;
var DropDownListIdentifier;
(function (DropDownListIdentifier) {
    DropDownListIdentifier[DropDownListIdentifier["Employees"] = 0] = "Employees";
    DropDownListIdentifier[DropDownListIdentifier["Suppliers"] = 1] = "Suppliers";
    DropDownListIdentifier[DropDownListIdentifier["Categories"] = 2] = "Categories";
    DropDownListIdentifier[DropDownListIdentifier["Customers"] = 3] = "Customers";
})(DropDownListIdentifier = exports.DropDownListIdentifier || (exports.DropDownListIdentifier = {}));
//# sourceMappingURL=order-details-models.js.map