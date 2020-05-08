import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';

@Component({
  selector: 'app-base-form',
  template: `<div></div>`
})
export abstract class BaseFormComponent implements OnInit {

  form: FormGroup;
  isLoading = false;

  constructor() { }

  ngOnInit() {
  }

  abstract submitForm();

  onSubmit() {
    if (this.form.valid) {
      this.isLoading = true;
      setTimeout(() => {
        this.isLoading = false;
      }, 10000);
      this.submitForm();
    } else {
      this.verifyForm(this.form);
    }
  }

  verifyForm(formGroup: FormGroup | FormArray) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      control.markAsDirty();
      control.markAsTouched();
      if (control instanceof FormGroup || control instanceof FormArray) {
        this.verifyForm(control);
      }
    });
  }

  resetForm() {
    this.form.reset();
  }

  verifyValidTouched(field: string) {
    return (
      !this.form.get(field).valid &&
      (this.form.get(field).touched || this.form.get(field).dirty)
    );
  }

  applyCssError(field: string) {
    return {
      'is-invalid': this.verifyValidTouched(field)
    };
  }


}
