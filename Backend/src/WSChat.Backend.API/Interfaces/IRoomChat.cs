using System;
using System.Collections.Generic;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.API.Interfaces
{
    public interface IRoomChat
    {
        public int GetIdByName(string roomName);

        public IList<Room> GetAllRooms();

        public void CreateRoom(string name);
    }
}
