import { FormGroup } from "@angular/forms";

export function firstValueMustBeGreaterThanSecondValueValidator(firstValueKey: string, secondValueKey: string) {
  return (f: FormGroup) => {
    let unitsInStock = +f.get(firstValueKey).value;
    let unitsOnOrder = +f.get(secondValueKey).value;

    //letting 'Required take charge from here!
    if (isNaN(unitsInStock) || isNaN(unitsOnOrder)
      //|| unitsInStock === 0 || unitsOnOrder === 0
    )
      return null;

    let result = unitsInStock >= unitsOnOrder && unitsOnOrder > 0 && unitsInStock > 0;
    return result ? null : {
      firstValueMustBeGreaterThanSecondValue: {
        valid: false,
      }
    }
  }
}
