import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ChatService } from '../../helpers/services/chat.service';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
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
  users: UserInterface[] = [];
  user: UserInterface;

  destroyed$ = new Subject();
  subjectReset: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reset$: Observable<boolean> = this.subjectReset.asObservable();
  subjectTargetUser: BehaviorSubject<UserInterface> = new BehaviorSubject<UserInterface>(null);
  targetUser$: Observable<UserInterface> = this.subjectTargetUser.asObservable();

  constructor(
    private webSocket: WebsocketService,
    private dataService: DataStorageService,
    private chatService: ChatService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.user = this.dataService.getUser();
    this.users = this.getUsers(this.dataService.getUsers());

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

  handlePayload(msg: MessageInterface) {
    if (msg && msg.event === EventEnum.Messaging && msg.user !== undefined) {
      this.messages.push(msg);
      this.subjectReset.next(true);
    } else if (
      msg &&
      msg.event === EventEnum.UpdateUserList &&
      msg.room !== undefined &&
      msg.room.users !== undefined
    ) {
      this.users = this.getUsers(msg.room.users);
    }
  }

  onSendMessage(message: MessageInterface) {
    this.webSocket.send(message);
  }

  getUsers(users: UserInterface[]) {
    return users.filter((u) => u.id !== this.user.id);
  }

  onSelectUser(event) {
    this.subjectTargetUser.next(event.user);
  }

  onCollapseMenu(event) {
    this.isCollapsed = event;
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
