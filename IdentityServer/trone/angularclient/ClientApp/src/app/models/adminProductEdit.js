"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var adminProductListItem_1 = require("./adminProductListItem");
var adminProductEdit = /** @class */ (function () {
    function adminProductEdit() {
    }
    adminProductEdit.prototype.toAdminProductListItem = function () {
        var listItem = new adminProductListItem_1.adminProductListItem();
        listItem.code = this.code;
        listItem.categoryId = this.categoryId;
        listItem.description = this.description;
        listItem.price = this.price;
        listItem.quantity = this.quantity;
        listItem.photoUrl = this.photoUrl;
        return listItem;
    };
    return adminProductEdit;
}());
exports.adminProductEdit = adminProductEdit;
//# sourceMappingURL=adminProductEdit.js.map