import { Injectable } from '@angular/core';
import { DataStorageService } from '../../../helpers/services/data-storage.service';
import { MessageService } from '../../../helpers/services/message.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ConnectionService {
  constructor(
    private dataService: DataStorageService,
    private messageService: MessageService,
    private router: Router
  ) {}

  HandleDisconnection() {
    this.dataService.removeAll();
    this.messageService.presentToast('warning', 'You are disconnected.');
    this.router.navigate(['chat', 'register']);
  }

  HandleConnection() {
    this.dataService.removeAll();
    this.messageService.presentToast('info', 'You are connected.');
    this.router.navigate(['chat', 'register']);
  }
}
