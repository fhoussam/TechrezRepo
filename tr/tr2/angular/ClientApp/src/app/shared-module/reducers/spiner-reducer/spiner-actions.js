"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SUCCESS = 'SUCCESS';
exports.ERROR = 'ERROR';
exports.ALERT = 'ALERT';
exports.PENDING = 'PENDING';
var RemoteCallAction = /** @class */ (function () {
    function RemoteCallAction(payload) {
        this.payload = payload;
        this.type = payload.messageType;
    }
    return RemoteCallAction;
}());
exports.RemoteCallAction = RemoteCallAction;
//# sourceMappingURL=spiner-actions.js.map