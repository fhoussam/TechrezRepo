"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function shouldBeLessThanValidator(maxValue) {
    return function (c) {
        //letting 'Required' take charge from here!
        if (c.value === null)
            return null;
        var result = +c.value < maxValue;
        return result ? null : {
            shouldBeLessThan: {
                maxValue: maxValue - 1,
                valid: false
            }
        };
    };
}
exports.shouldBeLessThanValidator = shouldBeLessThanValidator;
//# sourceMappingURL=shouldBeLessThanValidator.js.map