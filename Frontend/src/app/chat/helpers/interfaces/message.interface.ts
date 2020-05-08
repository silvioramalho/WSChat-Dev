import { UserInterface } from './user.interface';
import { RoomInterface } from './room.interface';
import { EventEnum } from '../enums/event.enum';

export interface MessageInterface {
  id?: string;
  event: EventEnum;
  sendDate?: Date;
  user?: UserInterface;
  room?: RoomInterface;
  messageText?: string;
  targetUserId?: string;
  isPrivate?: boolean;
  availableRooms?: RoomInterface[];
  targetUser?: UserInterface;
}



