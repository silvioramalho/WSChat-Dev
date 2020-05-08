import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { WebsocketService } from '../../helpers/services/websocket.service';
import { Subject, Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { BaseFormComponent } from '../../../shared/base-form.component';
import { MessageInterface } from '../../helpers/interfaces/message.interface';
import { EventEnum } from '../../helpers/enums/event.enum';
import { RegisterUserService } from '../../helpers/services/register-user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent extends BaseFormComponent
  implements OnInit, OnDestroy {
  validateForm: FormGroup;
  messages: string[] = [];
  destroyed$ = new Subject();

  constructor(
    private fb: FormBuilder,
    private webSocket: WebsocketService,
    private userService: RegisterUserService,
    private router: Router
  ) {
    super();
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      nickname: [
        null,
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
    });

    this.webSocket.connection$.pipe(takeUntil(this.destroyed$)).subscribe(
      (msg) => {
        console.log('Message received:', msg);
        if (msg) {
          this.handlePayload(msg);
        }

      },
      (error) => {
        return console.log('Some error on connection register', error);
      },
      () => console.log('complete register')
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
      console.log(this.validateForm.value.nickname);
      this.sendMessage(this.validateForm.value.nickname);
    }
  }

  sendMessage(message: string) {
    this.webSocket.send({
      event: EventEnum.RegisterUser,
      messageText: message,
    });
  }

  handlePayload(msg: MessageInterface)
  {
    if (msg && msg.event === 2 && msg.user !== undefined) {
      this.userService.handlePayload(msg);
      this.router.navigate(['chat', 'rooms']);
    }
  }

  ngOnDestroy() {
    this.destroyed$.next();
  }
}
