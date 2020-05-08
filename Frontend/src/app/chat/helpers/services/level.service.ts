import { Injectable } from '@angular/core';
import { DataStorageService } from '../../../helpers/services/data-storage.service';
import { MessageService } from '../../../helpers/services/message.service';

@Injectable({
  providedIn: 'root'
})
export class LevelService {

  constructor(private dataService: DataStorageService, private messageService: MessageService) { }

  isRegistrered(){
    return !!this.dataService.getUser();
  }

}
