import { Component, OnInit, Input } from '@angular/core';
import { RoomInterface } from '../../../helpers/interfaces/room.interface';

@Component({
  selector: 'app-room-card',
  templateUrl: './room-card.component.html',
  styleUrls: ['./room-card.component.scss'],
})
export class RoomCardComponent implements OnInit {
  @Input() room: RoomInterface;

  constructor() {}

  ngOnInit(): void {}

  getCountUsers() {
    if (this.room.users) {
      return this.room.users.length;
    } else {
      return 0;
    }
  }
}
