import { AbstractControl, ValidatorFn } from "@angular/forms";

export function matchValidator(formControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): {[key: string]: any} | null => {
      const match = formControl.value == control.value;
      return !match ? {match: "Passwords must match."} : null;
    };
  }