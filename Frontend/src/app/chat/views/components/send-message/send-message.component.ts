import { Component, OnInit, Output, EventEmitter, Input, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { EventEnum } from '../../../helpers/enums/event.enum';
import { UserInterface } from '../../../helpers/interfaces/user.interface';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-send-message',
  templateUrl: './send-message.component.html',
  styleUrls: ['./send-message.component.scss']
})
export class SendMessageComponent implements OnInit, OnDestroy {
  @Output() sendMessage =  new EventEmitter();
  @Input() users: UserInterface[] = [];
  @Input() reset$: Observable<boolean>;
  @Input() targetUser$: Observable<UserInterface>;

  validateForm: FormGroup;
  subs: Subscription[] = [];


  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {

    this.validateForm = this.fb.group({
      message: [null, [Validators.required, Validators.maxLength(200)]],
      isPrivate: [null],
      targetUserId: [''],
    });

    this.subs.push(
      this.targetUser$.subscribe(user => {
        if (user){
          this.validateForm.get('targetUserId').patchValue(user.id);
        }
      })
    );

    this.subs.push(
      this.reset$.subscribe(b => {
        if (b){
          this.resetForm();
        }
      })
    );
  }

  verifyForm(formGroup: FormGroup | FormArray) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      control.markAsDirty();
      control.markAsTouched();
      control.updateValueAndValidity();
      if (control instanceof FormGroup || control instanceof FormArray) {
        this.verifyForm(control);
      }
    });
  }

  submitForm(): void {
    this.verifyForm(this.validateForm);

    if (this.validateForm.valid) {
      console.log('message', this.validateForm.value);
      this.send(
        this.validateForm.value.message,
        this.validateForm.value.targetUserId,
        this.validateForm.value.isPrivate
      );
    }
  }

  resetForm(): void {
    this.validateForm.reset();
  }

  send(message: string, targetUserId?: string, isPrivate?: boolean) {
    this.sendMessage.emit({
      event: EventEnum.Messaging,
      messageText: message,
      targetUserId,
      isPrivate
    });
  }

  onPressEnter($event: KeyboardEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    this.submitForm();
  }

  ngOnDestroy() {
    this.subs.forEach(s => s.unsubscribe());
  }

}
