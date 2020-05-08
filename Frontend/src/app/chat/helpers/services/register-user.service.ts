import { Injectable } from '@angular/core';
import { MessageInterface } from '../interfaces/message.interface';
import { EventEnum } from '../enums/event.enum';
import { DataStorageService } from '../../../helpers/services/data-storage.service';

@Injectable({
  providedIn: 'root',
})
export class RegisterUserService {
  constructor(private dataService: DataStorageService) {}

  handlePayload(msg: MessageInterface) {
    if (msg.event === EventEnum.RegisterUser) {
      if (msg.user !== undefined) {
        this.dataService.setUser(msg.user);
        this.dataService.setIdConnection(msg.user.id);
      }
      if (msg.availableRooms !== undefined){
        this.dataService.setRooms(msg.availableRooms);
      }
    }
  }
}
