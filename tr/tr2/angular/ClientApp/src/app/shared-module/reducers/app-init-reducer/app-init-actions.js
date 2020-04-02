"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.INIT_APP_BEGIN = "INIT_APP_BEGIN";
exports.INIT_ANTIFORGERY_BEGIN = "INIT_ANTIFORGERY_BEGIN";
exports.INIT_ANTIFORGERY_END = "INIT_ANTIFORGERY_END";
exports.INIT_CATEGORIES_BEGIN = "INIT_CATEGORIES_BEGIN";
exports.INIT_CATEGORIES_END = "INIT_CATEGORIES_END";
exports.INIT_APP_END = "INIT_APP_END";
var InitAppBegin = /** @class */ (function () {
    function InitAppBegin() {
        this.type = "INIT_APP_BEGIN";
    }
    return InitAppBegin;
}());
exports.InitAppBegin = InitAppBegin;
var InitAntiForgeryBegin = /** @class */ (function () {
    function InitAntiForgeryBegin() {
        this.type = "INIT_ANTIFORGERY_BEGIN";
    }
    return InitAntiForgeryBegin;
}());
exports.InitAntiForgeryBegin = InitAntiForgeryBegin;
var InitAntiForgeryEnd = /** @class */ (function () {
    function InitAntiForgeryEnd() {
        this.type = "INIT_ANTIFORGERY_END";
    }
    return InitAntiForgeryEnd;
}());
exports.InitAntiForgeryEnd = InitAntiForgeryEnd;
var InitCategoriesBegin = /** @class */ (function () {
    function InitCategoriesBegin() {
        this.type = "INIT_CATEGORIES_BEGIN";
    }
    return InitCategoriesBegin;
}());
exports.InitCategoriesBegin = InitCategoriesBegin;
var InitCategoriesEnd = /** @class */ (function () {
    function InitCategoriesEnd(payload) {
        this.payload = payload;
        this.type = "INIT_CATEGORIES_END";
    }
    return InitCategoriesEnd;
}());
exports.InitCategoriesEnd = InitCategoriesEnd;
var InitAppEnd = /** @class */ (function () {
    function InitAppEnd() {
        this.type = "INIT_APP_END";
    }
    return InitAppEnd;
}());
exports.InitAppEnd = InitAppEnd;
//# sourceMappingURL=app-init-actions.js.map