"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function firstValueMustBeGreaterThanSecondValueValidator(firstValueKey, secondValueKey) {
    return function (f) {
        var unitsInStock = +f.get(firstValueKey).value;
        var unitsOnOrder = +f.get(secondValueKey).value;
        //letting 'Required take charge from here!
        if (isNaN(unitsInStock) || isNaN(unitsOnOrder) || unitsInStock === 0 || unitsOnOrder === 0)
            return null;
        var result = unitsInStock >= unitsOnOrder;
        return result ? null : {
            firstValueMustBeGreaterThanSecondValue: {
                valid: false,
            }
        };
    };
}
exports.firstValueMustBeGreaterThanSecondValueValidator = firstValueMustBeGreaterThanSecondValueValidator;
//# sourceMappingURL=firstValueMustBeGreaterThanSecondValueValidator.js.map