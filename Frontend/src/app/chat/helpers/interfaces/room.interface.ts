import { UserInterface } from './user.interface';

export interface RoomInterface {
  id: number;
  name: string;
  users: UserInterface[];
}
