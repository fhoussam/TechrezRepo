"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SEARCH_ORDER_DETAILS_BEGIN = "SEARCH_ORDER_DETAILS_BEGIN";
exports.SEARCH_ORDER_DETAILS_END = "SEARCH_ORDER_DETAILS_END";
exports.SELECT_ORDER_DETAILS_BEGIN = "SELECT_ORDER_DETAILS_BEGIN";
exports.SELECT_ORDER_DETAILS_END_FOR_DISPLAY = "SELECT_ORDER_DETAILS_END_FOR_DISPLAY";
exports.SELECT_ORDER_DETAILS_END_FOR_EDIT = "SELECT_ORDER_DETAILS_END_FOR_EDIT";
exports.SHOWINFO_ORDER_DETAILS_BEGIN = "SHOWINFO_ORDER_DETAILS_BEGIN";
exports.SHOWINFO_ORDER_DETAILS_END = "SHOWINFO_ORDER_DETAILS_END";
exports.EDIT_ORDER_DETAILS_BEGIN = "EDIT_ORDER_DETAILS_BEGIN";
exports.DELETE_ORDER_DETAILS_BEGIN = "DELETE_ORDER_DETAILS_BEGIN";
exports.DELETE_ORDER_DETAILS_END = "DELETE_ORDER_DETAILS_END";
var SearchOrderDetailsBegin = /** @class */ (function () {
    function SearchOrderDetailsBegin(payload) {
        this.payload = payload;
        this.type = exports.SEARCH_ORDER_DETAILS_BEGIN;
    }
    return SearchOrderDetailsBegin;
}());
exports.SearchOrderDetailsBegin = SearchOrderDetailsBegin;
var SearchOrderDetailsEnd = /** @class */ (function () {
    function SearchOrderDetailsEnd(payload) {
        this.payload = payload;
        this.type = exports.SEARCH_ORDER_DETAILS_END;
    }
    return SearchOrderDetailsEnd;
}());
exports.SearchOrderDetailsEnd = SearchOrderDetailsEnd;
var SelectOrderDetailsBegin = /** @class */ (function () {
    function SelectOrderDetailsBegin(orderId, productId, forEdit) {
        this.orderId = orderId;
        this.productId = productId;
        this.forEdit = forEdit;
        this.type = exports.SELECT_ORDER_DETAILS_BEGIN;
    }
    return SelectOrderDetailsBegin;
}());
exports.SelectOrderDetailsBegin = SelectOrderDetailsBegin;
var SelectOrderDetailsEndForDisplay = /** @class */ (function () {
    function SelectOrderDetailsEndForDisplay(payload) {
        this.payload = payload;
        this.type = exports.SELECT_ORDER_DETAILS_END_FOR_DISPLAY;
    }
    return SelectOrderDetailsEndForDisplay;
}());
exports.SelectOrderDetailsEndForDisplay = SelectOrderDetailsEndForDisplay;
var SelectOrderDetailsEndForEdit = /** @class */ (function () {
    function SelectOrderDetailsEndForEdit(payload) {
        this.payload = payload;
        this.type = exports.SELECT_ORDER_DETAILS_END_FOR_EDIT;
    }
    return SelectOrderDetailsEndForEdit;
}());
exports.SelectOrderDetailsEndForEdit = SelectOrderDetailsEndForEdit;
var ShowinfoOrderDetailsBegin = /** @class */ (function () {
    function ShowinfoOrderDetailsBegin() {
        this.type = exports.SHOWINFO_ORDER_DETAILS_BEGIN;
    }
    return ShowinfoOrderDetailsBegin;
}());
exports.ShowinfoOrderDetailsBegin = ShowinfoOrderDetailsBegin;
var ShowinfoOrderDetailsEnd = /** @class */ (function () {
    function ShowinfoOrderDetailsEnd(payload) {
        this.payload = payload;
        this.type = exports.SHOWINFO_ORDER_DETAILS_END;
    }
    return ShowinfoOrderDetailsEnd;
}());
exports.ShowinfoOrderDetailsEnd = ShowinfoOrderDetailsEnd;
var EditOrderDetailsBegin = /** @class */ (function () {
    function EditOrderDetailsBegin(payload) {
        this.payload = payload;
        this.type = exports.EDIT_ORDER_DETAILS_BEGIN;
    }
    return EditOrderDetailsBegin;
}());
exports.EditOrderDetailsBegin = EditOrderDetailsBegin;
var DeleteOrderDetailsBegin = /** @class */ (function () {
    function DeleteOrderDetailsBegin() {
        this.type = exports.DELETE_ORDER_DETAILS_BEGIN;
    }
    return DeleteOrderDetailsBegin;
}());
exports.DeleteOrderDetailsBegin = DeleteOrderDetailsBegin;
var DeleteOrderDetailsEnd = /** @class */ (function () {
    function DeleteOrderDetailsEnd(payload) {
        this.payload = payload;
        this.type = exports.DELETE_ORDER_DETAILS_END;
    }
    return DeleteOrderDetailsEnd;
}());
exports.DeleteOrderDetailsEnd = DeleteOrderDetailsEnd;
//# sourceMappingURL=order-details-actions.js.map