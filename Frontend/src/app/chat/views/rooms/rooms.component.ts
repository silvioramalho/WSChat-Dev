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
import { NzModalService } from 'ng-zorro-antd/modal';
import { RoomAddComponent } from '../components/room-add/room-add.component';

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
    private router: Router,
    private modalService: NzModalService
  ) {}

  ngOnInit(): void {
    this.rooms = this.dataService.getRooms();
    this.user = this.dataService.getUser();

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
    } else if (msg && msg.event === EventEnum.UpdateRoomList && msg.availableRooms) {
      this.rooms = msg.availableRooms;
    } else if (msg && msg.event === EventEnum.UpdateUsersRooms && msg.availableRooms) {
      this.rooms = msg.availableRooms;
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

  onExit() {
    window.location.reload();
  }

  ngOnDestroy() {
    this.destroyed$.next();
  }

  showModalAddRoom(): void {
    this.modalService.create({
      nzTitle: 'Add new room',
      nzContent: RoomAddComponent,
      nzFooter: null
    });
  }
}
