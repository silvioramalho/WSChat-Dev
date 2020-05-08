import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormArray } from '@angular/forms';
import { Subject } from 'rxjs';
import { MessageInterface } from '../../../helpers/interfaces/message.interface';
import { EventEnum } from 'src/app/chat/helpers/enums/event.enum';
import { takeUntil } from 'rxjs/operators';
import { WebsocketService } from '../../../helpers/services/websocket.service';
import { NzModalService, NzModalRef } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-room-add',
  templateUrl: './room-add.component.html',
  styleUrls: ['./room-add.component.scss'],
})
export class RoomAddComponent implements OnInit, OnDestroy {
  validateForm: FormGroup;
  messages: string[] = [];
  destroyed$ = new Subject();

  constructor(
    private fb: FormBuilder,
    private webSocket: WebsocketService,
    private modalService: NzModalService,
    private modal: NzModalRef
  ) {}

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      name: [
        null,
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(10),
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
        return error;
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
      console.log(this.validateForm.value.name);
      this.sendMessage(this.validateForm.value.name);
    }
  }

  handlePayload(msg: MessageInterface) {
    if (msg && msg.event === EventEnum.CreateRoom && msg.user !== undefined) {
      this.modal.destroy();
    }
  }

  sendMessage(message: string) {
    this.webSocket.send({
      event: EventEnum.CreateRoom,
      messageText: message,
    });
  }

  ngOnDestroy() {
    this.destroyed$.next();
  }
}
