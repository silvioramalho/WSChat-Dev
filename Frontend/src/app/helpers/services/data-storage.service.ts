import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DataStorageService {
  private idConnection = 'idConnection';
  private rooms = 'availableRooms';
  private user = 'user';
  private roomName = 'roomName';
  private users = 'users';
  private list4delete: string[] = [];

  constructor() {
    this.list4delete.push(this.idConnection);
    this.list4delete.push(this.rooms);
    this.list4delete.push(this.user);
    this.list4delete.push(this.users);
    this.list4delete.push(this.roomName);
  }

  // SETS
  setIdConnection(idConnection) {
    localStorage.setItem(this.idConnection, idConnection);
  }

  setRooms(rooms) {
    const payload = JSON.stringify(rooms);
    localStorage.setItem(this.rooms, payload);
  }

  setUser(user) {
    const payload = JSON.stringify(user);
    localStorage.setItem(this.user, payload);
  }

  setUsers(users) {
    const payload = JSON.stringify(users);
    localStorage.setItem(this.users, payload);
  }

  setRoomName(roomName) {
    localStorage.setItem(this.roomName, roomName);
  }

  // GETS

  getIdConnection() {
    return JSON.parse(localStorage.getItem(this.idConnection));
  }

  getRooms() {
    return JSON.parse(localStorage.getItem(this.rooms));
  }

  getUser() {
    return JSON.parse(localStorage.getItem(this.user));
  }

  getUsers() {
    return JSON.parse(localStorage.getItem(this.users));
  }

  getRoomName() {
    return localStorage.getItem(this.roomName);
  }

  // REMOVE

  removeIdConnection(){
    localStorage.removeItem(this.idConnection);
  }

  removeRooms(){
    localStorage.removeItem(this.rooms);
  }

  removeUser(){
    localStorage.removeItem(this.user);
  }

  removeUsers(){
    localStorage.removeItem(this.users);
  }

  removeRoomName(){
    localStorage.removeItem(this.roomName);
  }

  removeAll() {
    this.list4delete.forEach(element => {
      localStorage.removeItem(element);
    });
    localStorage.clear();
  }
}
