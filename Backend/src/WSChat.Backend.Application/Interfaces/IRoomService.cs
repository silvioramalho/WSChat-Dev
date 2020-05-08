using System;
using System.Collections.Generic;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Interfaces
{
    public interface IRoomService
    {
        public int GetIdByName(string roomName);

        public IList<Room> GetAllRooms();

        public int CreateRoom(string name);

        public Room GetRoomById(int id);

        public void UpdateRoom(Room room);

        public bool ExistOtherUserRoom(string idUser);

        public bool IsUniqueRoomName(string name);
    }
}
