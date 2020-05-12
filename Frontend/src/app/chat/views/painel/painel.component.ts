import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ChatService } from '../../helpers/services/chat.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { EventEnum } from '../../helpers/enums/event.enum';
import { Router } from '@angular/router';
import { MessageInterface } from '../../helpers/interfaces/message.interface';
import { WebsocketService } from '../../helpers/services/websocket.service';
import { UserInterface } from '../../helpers/interfaces/user.interface';
import { DataStorageService } from '../../../helpers/services/data-storage.service';
import { MessageService } from '../../../helpers/services/message.service';

@Component({
  selector: 'app-painel',
  templateUrl: './painel.component.html',
  styleUrls: ['./painel.component.scss'],
})
export class PainelComponent implements OnInit, OnDestroy {
  messages: MessageInterface[] = [];
  isCollapsed = false;
  radioValue = '';

  validateForm: FormGroup;
  destroyed$ = new Subject();
  users: UserInterface[] = [];
  user: UserInterface;

  constructor(
    private fb: FormBuilder,
    private webSocket: WebsocketService,
    private dataService: DataStorageService,
    private chatService: ChatService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.user = this.dataService.getUser();
    this.users = this.getUsers(this.dataService.getUsers());

    this.validateForm = this.fb.group({
      message: [null, [Validators.required, Validators.maxLength(200)]],
      isPrivate: [null],
      targetUserId: [''],
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
      console.log('message', this.validateForm.value);
      this.sendMessage(
        this.validateForm.value.message,
        this.validateForm.value.targetUserId,
        this.validateForm.value.isPrivate
      );
    }
  }

  resetForm(): void {
    this.validateForm.reset();
  }

  handlePayload(msg: MessageInterface) {
    if (msg && msg.event === EventEnum.Messaging && msg.user !== undefined) {
      this.messages.push(msg);
      this.resetForm();
    } else if (
      msg &&
      msg.event === EventEnum.UpdateUserList &&
      msg.room !== undefined &&
      msg.room.users !== undefined
    ) {
      this.users = this.getUsers(msg.room.users);
    }
  }

  sendMessage(message: string, targetUserId?: string, isPrivate?: boolean) {
    this.webSocket.send({
      event: EventEnum.Messaging,
      messageText: message,
      targetUserId,
      isPrivate,
    });
  }

  getUsers(users: UserInterface[]) {
    return users.filter((u) => u.id !== this.user.id);
  }

  onSelectUser(event) {
    this.validateForm.get('targetUserId').patchValue(event.user.id);
  }
  onCollapseMenu(event) {
    this.isCollapsed = event;
  }

  onPressEnter($event: KeyboardEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    this.submitForm();
  }

  sendExit() {
    this.webSocket.send({
      event: EventEnum.ExitRoom,
    });
  }

  clearDataStorage() {
    this.dataService.removeRoomName();
    this.user.idActiveRoom = null;
    this.dataService.setUser(this.user);
  }

  onExit() {
    this.clearDataStorage();
    this.sendExit();
    this.router.navigate(['chat', 'rooms'], {
      replaceUrl: true,
    });
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
