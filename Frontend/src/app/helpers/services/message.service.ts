import { Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private msgService: NzMessageService) { }

  public presentToast(type: string, message): void {
    this.msgService.create(type, message);
  }
}

