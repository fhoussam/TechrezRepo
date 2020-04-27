"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var GridField = /** @class */ (function () {
    function GridField(fieldName, fieldDescription, sortfieldIndex, hidden) {
        if (hidden === void 0) { hidden = false; }
        this.fieldName = fieldName;
        this.fieldDescription = fieldDescription;
        this.sortfieldIndex = sortfieldIndex;
        this.hidden = hidden;
    }
    return GridField;
}());
exports.GridField = GridField;
//# sourceMappingURL=GridField.js.map