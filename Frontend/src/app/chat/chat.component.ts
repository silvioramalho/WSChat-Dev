import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { WebsocketService } from './helpers/services/websocket.service';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { MessageInterface } from './helpers/interfaces/message.interface';
import { EventEnum } from './helpers/enums/event.enum';
import { MessageService } from '../helpers/services/message.service';
import { DataStorageService } from '../helpers/services/data-storage.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit, OnDestroy {
  messages: string[] = [];
  destroyed$ = new Subject();

  constructor(
    private webSocket: WebsocketService,
    private messageService: MessageService,
    private dataService: DataStorageService
  ) {}

  ngOnInit(): void {
    this.webSocket
      .connect(environment.webSocketServer)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (msg) => {
          this.handleRequestMessage(msg);
        },
        (error) => console.log('Some error on connection', error),
        () => console.log('complete topo')
      );
  }

  ngOnDestroy() {
    this.destroyed$.next();
  }

  handleRequestMessage(msg: MessageInterface) {
    switch (msg.event) {
      case EventEnum.Error:
        this.messageService.presentToast('error', msg.messageText);
        break;
      case EventEnum.CreateRoom:
        this.messageService.presentToast('info', msg.messageText);
        break;
      case EventEnum.UpdateUserList:
        this.messageService.presentToast('info', msg.messageText);
        break;
      case EventEnum.UpdateRoomList:
        this.messageService.presentToast('info', msg.messageText);
        if (msg.availableRooms) {
          this.dataService.setRooms(msg.availableRooms);
        }
        break;
      default:
        break;
    }
  }
}
