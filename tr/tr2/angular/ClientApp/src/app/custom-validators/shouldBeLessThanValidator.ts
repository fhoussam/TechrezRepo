import { ValidatorFn, FormControl } from "@angular/forms";

export function shouldBeLessThanValidator(maxValue: number): ValidatorFn /*we got that  from angular's Validators.minLength definition */ {
  return (c: FormControl) => {

    //letting 'Required' take charge from here!
    if (c.value === null)
      return null;

    let result: boolean = +c.value <= maxValue;
    return result ? null : {
      shouldBeLessThan: {
        maxValue: maxValue,
        valid: false
      }
    }
  }
}
