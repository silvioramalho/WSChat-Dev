import { Component, OnInit, OnDestroy } from '@angular/core';
import { RoomInterface } from '../../helpers/interfaces/room.interface';
import { DataStorageService } from '../../../helpers/services/data-storage.service';
import { Subject } from 'rxjs';
import { WebsocketService } from '../../helpers/services/websocket.service';
import { takeUntil } from 'rxjs/operators';
import { EventEnum } from '../../helpers/enums/event.enum';
import { MessageInterface } from '../../helpers/interfaces/message.interface';
import { RoomService } from '../../helpers/services/room.service';
import { Router } from '@angular/router';
import { UserInterface } from '../../helpers/interfaces/user.interface';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.scss'],
})
export class RoomsComponent implements OnInit, OnDestroy {
  rooms: RoomInterface[] = [];
  user: UserInterface;
  destroyed$ = new Subject();

  constructor(
    private dataService: DataStorageService,
    private webSocket: WebsocketService,
    private roomService: RoomService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.rooms = this.dataService.getRooms();
    this.user = this.dataService.getUser();
    console.log(this.rooms);

    this.webSocket.connection$.pipe(takeUntil(this.destroyed$)).subscribe(
      (msg) => {
        console.log('Message received:', msg);
        if (msg) {
          this.handlePayload(msg);
        }
      },
      (error) => error,
      () => console.log('complete room')
    );
  }

  sendMessage(message: string) {
    this.webSocket.send({
      event: EventEnum.EnterRoom,
      messageText: message,
    });
  }

  handlePayload(msg: MessageInterface) {
    if (msg && msg.event === EventEnum.WelcomeMessage) {
      this.roomService.handlePayload(msg);
      this.router.navigate(['chat', 'painel']);
    }
  }

  enterRoom(idRoom: number) {
    console.log('this.dataService.getUser()', this.user);
    if (this.user.idActiveRoom === idRoom) {
      this.router.navigate(['chat', 'painel']);
    } else {
      this.sendMessage(idRoom.toString());
    }
  }

  ngOnDestroy() {
    this.destroyed$.next();
  }
}
