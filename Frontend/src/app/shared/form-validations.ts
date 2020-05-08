import {
  FormArray,
  FormControl,
  FormGroup
} from '@angular/forms';

export class FormValidations {
  static requiredMinCheckbox(min = 1) {
    const validator = (formArray: FormArray) => {
      const totalChecked = formArray.controls
        .map((v) => v.value)
        .reduce((total, current) => (current ? total + current : total), 0);
      return totalChecked >= min ? null : { required: true };
    };
    return validator;
  }

  static equalsTo(otherField: string) {
    const validator = (formControl: FormControl) => {
      if (otherField == null) {
        throw new Error('You must enter a field.');
      }

      if (!formControl.root || !(formControl.root as FormGroup).controls) {
        return null;
      }

      const field = (formControl.root as FormGroup).get(otherField);

      if (!field) {
        throw new Error('You must enter a valid field.');
      }

      if (field.value !== formControl.value) {
        return { equalsTo: otherField };
      }

      return null;
    };
    return validator;
  }

  static getErrorMsg(
    fieldName: string,
    validatorValue?: any,
  ) {
    const config = {
      required: `Enter ${fieldName}.`,
      minlength: `Enter at least ${validatorValue.requiredLength} characters.`,
      maxlength: `Enter a maximum of ${validatorValue.requiredLength} characters.`,
    };
  }
}
