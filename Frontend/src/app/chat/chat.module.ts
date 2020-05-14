import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './views/register/register.component';
import { ChatComponent } from './chat.component';
import { RoomsComponent } from './views/rooms/rooms.component';
import { ChatRoutingModule } from './chat-routing.module';
import { PainelComponent } from './views/painel/painel.component';
import { SharedModule } from '../shared/shared.module';
import { RoomCardComponent } from './views/components/room-card/room-card.component';
import { UserListComponent } from './views/components/user-list/user-list.component';
import { ChatMessagesComponent } from './views/components/chat-messages/chat-messages.component';
import { RoomAddComponent } from './views/components/room-add/room-add.component';
import { SendMessageComponent } from './views/components/send-message/send-message.component';
import { ChatHeaderComponent } from './views/components/chat-header/chat-header.component';

@NgModule({
  declarations: [
    RegisterComponent,
    ChatComponent,
    RoomsComponent,
    PainelComponent,
    RoomCardComponent,
    UserListComponent,
    ChatMessagesComponent,
    RoomAddComponent,
    SendMessageComponent,
    ChatHeaderComponent,
  ],
  entryComponents: [RoomCardComponent, ChatMessagesComponent, RoomAddComponent],
  imports: [CommonModule, SharedModule, ChatRoutingModule],
})
export class ChatModule {}
