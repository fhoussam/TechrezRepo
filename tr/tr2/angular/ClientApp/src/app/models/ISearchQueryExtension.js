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
var SearchQueryExtension = /** @class */ (function () {
    function SearchQueryExtension(defaultSortField) {
        this.isDesc = false;
        this.pageIndex = 0;
        this.sortField = defaultSortField;
    }
    SearchQueryExtension.prototype.isEmptyQuery = function () {
        var tmp = __assign({}, this);
        delete tmp['isDesc'];
        delete tmp['pageIndex'];
        delete tmp['sortField'];
        var emptyObject = JSON.parse(JSON.stringify(tmp, function (key, value) {
            if (value !== null)
                return value;
        }));
        return Object.keys(emptyObject).length === 0;
    };
    return SearchQueryExtension;
}());
exports.SearchQueryExtension = SearchQueryExtension;
//# sourceMappingURL=ISearchQueryExtension.js.map