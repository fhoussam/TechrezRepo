"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SUCCESS = 'SUCCESS';
exports.ERROR = 'ERROR';
exports.ALERT = 'ALERT';
exports.PENDING = 'PENDING';
exports.CONFIRM = 'CONFIRM';
var SuccessAction = /** @class */ (function () {
    function SuccessAction() {
        this.type = exports.SUCCESS;
    }
    return SuccessAction;
}());
exports.SuccessAction = SuccessAction;
var ErrorAction = /** @class */ (function () {
    function ErrorAction(payload) {
        this.payload = payload;
        this.type = exports.ERROR;
    }
    return ErrorAction;
}());
exports.ErrorAction = ErrorAction;
var AlertAction = /** @class */ (function () {
    function AlertAction(payload) {
        this.payload = payload;
        this.type = exports.ALERT;
    }
    return AlertAction;
}());
exports.AlertAction = AlertAction;
var PendingAction = /** @class */ (function () {
    function PendingAction(payload) {
        this.payload = payload;
        this.type = exports.PENDING;
    }
    return PendingAction;
}());
exports.PendingAction = PendingAction;
var ConfirmAction = /** @class */ (function () {
    function ConfirmAction(payload) {
        this.payload = payload;
        this.type = exports.CONFIRM;
    }
    return ConfirmAction;
}());
exports.ConfirmAction = ConfirmAction;
//# sourceMappingURL=spiner-actions.js.map