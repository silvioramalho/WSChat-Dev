import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { UserInterface } from '../../../helpers/interfaces/user.interface';

@Component({
  selector: 'app-chat-header',
  templateUrl: './chat-header.component.html',
  styleUrls: ['./chat-header.component.scss'],
})
export class ChatHeaderComponent implements OnInit {
  @Output() exitClick = new EventEmitter();
  @Input() user: UserInterface;

  constructor() {}

  ngOnInit(): void {}

  onExit(){
    this.exitClick.emit();
  }
}
