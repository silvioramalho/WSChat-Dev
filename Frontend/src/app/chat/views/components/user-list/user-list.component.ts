import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { UserInterface } from '../../../helpers/interfaces/user.interface';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
})
export class UserListComponent implements OnInit {
  @Output() selectValue = new EventEmitter();
  @Output() menuCollapse = new EventEmitter();
  @Input() users: UserInterface[] = [];

  @Input() isCollapsed: boolean;

  constructor() {}

  ngOnInit(): void {}

  selectUser(user: UserInterface) {
    console.log('select User', user);
    this.selectValue.emit({ user });
  }

  collapse() {
    this.isCollapsed = !this.isCollapsed;
    this.menuCollapse.emit(this.isCollapsed);
  }
}
