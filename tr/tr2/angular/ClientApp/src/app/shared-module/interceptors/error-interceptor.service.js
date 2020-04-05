"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ErrorInterceptorService = /** @class */ (function () {
    function ErrorInterceptorService(store, router) {
        this.store = store;
        this.router = router;
    }
    ErrorInterceptorService.prototype.intercept = function (req, next) {
        return next.handle(req);
    };
    return ErrorInterceptorService;
}());
exports.ErrorInterceptorService = ErrorInterceptorService;
//# sourceMappingURL=error-interceptor.service.js.map