import { Injectable } from '@angular/core';
import { MessageInterface } from '../interfaces/message.interface';
import { EventEnum } from '../enums/event.enum';
import { DataStorageService } from '../../../helpers/services/data-storage.service';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private dataService: DataStorageService) { }

  handlePayload(msg: MessageInterface) {
    if (msg.event === EventEnum.WelcomeMessage) {
      if (msg.user !== undefined) {
        this.dataService.setUser(msg.user);
      }
      if (msg.room !== undefined) {
        this.dataService.setRoomName(msg.room.name);
        if (msg.room.users !== undefined) {
          this.dataService.setUsers(msg.room.users);
        } else {
          this.dataService.setUsers([]);
        }

      }
    }
  }
}
