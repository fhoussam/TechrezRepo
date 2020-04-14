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
var APP_SETTINGS_1 = require("../models/APP_SETTINGS");
var HttpHelperService = /** @class */ (function () {
    function HttpHelperService(datepipe) {
        this.datepipe = datepipe;
    }
    HttpHelperService.prototype.toHttpParams = function (object) {
        var _this = this;
        var params = new http_1.HttpParams();
        Object.keys(object).forEach(function (item) {
            if (object[item]) {
                //is date, we can use typeof operator for other keys if we want later
                if (object[item].getMonth) {
                    params = params.set(item, _this.datepipe.transform(object[item], APP_SETTINGS_1.APP_SETTINGS.queryStringDateFormat));
                }
                else {
                    params = params.set(item, object[item]);
                }
            }
        });
        return params;
    };
    HttpHelperService = __decorate([
        core_1.Injectable()
    ], HttpHelperService);
    return HttpHelperService;
}());
exports.HttpHelperService = HttpHelperService;
//# sourceMappingURL=http-helper.js.map