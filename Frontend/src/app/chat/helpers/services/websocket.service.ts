import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { MessageInterface } from '../interfaces/message.interface';
import { EventEnum } from '../enums/event.enum';
import { LevelService } from './level.service';
import { ConnectionService } from './connection.service';

const RETRY_SECONDS = 10;

@Injectable({
  providedIn: 'root',
})
export class WebsocketService {

  public connection$: WebSocketSubject<MessageInterface>;

  constructor(private connectionService: ConnectionService) {}

  connect(apiUrl: string): Observable<MessageInterface> {
    if (this.connection$) {
      return this.connection$;
    } else {
      this.connection$ = webSocket({
        url: apiUrl,
        closeObserver: {
          next(closeEvent) {
            this.connectionService.HandleDisconnection();
          },
        },
        openObserver: {
          next: () => {
            this.connectionService.HandleConnection();
          },
        }
      });
      return this.connection$;
    }
  }

  send(data: MessageInterface) {
    if (this.connection$) {
      this.connection$.next(data);
    } else {
      console.error('Did not send data, open a connection first');
    }
  }

  closeConnection() {
    if (this.connection$) {
      this.connection$.complete();
      this.connection$ = null;
    }
  }
}
