import {
  Component,
  OnInit,
  AfterViewChecked,
  Input,
} from '@angular/core';
import { WebsocketService } from '../../../helpers/services/websocket.service';
import { MessageInterface } from '../../../helpers/interfaces/message.interface';

@Component({
  selector: 'app-chat-messages',
  templateUrl: './chat-messages.component.html',
  styleUrls: ['./chat-messages.component.scss'],
})
export class ChatMessagesComponent implements OnInit, AfterViewChecked {
  @Input() messages: MessageInterface[] = [];

  constructor(private webSocket: WebsocketService) {}

  ngOnInit(): void {}

  ngAfterViewChecked(): void {
    const container = document.getElementById('messageList');
    container.scrollTop = container.scrollHeight;
  }
}
