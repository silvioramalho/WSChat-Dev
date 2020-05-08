using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Services
{
    public class RoomService : IRoomService
    {
        private ConcurrentDictionary<int, Room> _rooms = new ConcurrentDictionary<int, Room>();

        public RoomService()
        {
            _rooms.TryAdd(1, new Room(1, "general"));
        }

        public int GetIdByName(string roomName)
        {
            return _rooms.FirstOrDefault(x => x.Value.Name == roomName).Key;
        }

        public IList<Room> GetAllRooms()
        {
            return _rooms.Select(p => p.Value).ToList();
        }

        public Room GetRoomById(int id)
        {
            return _rooms.FirstOrDefault(p => p.Value.Id == id).Value;
        }

        private int GenerateRoomId()
        {
            return _rooms.Max(p => p.Key) + 1;
        }

        public int CreateRoom(string name)
        {
            int newId = GenerateRoomId();
            _rooms.TryAdd(newId, new Room(newId, name));
            return newId;
        }

        public void UpdateRoom(Room room)
        {
            Room oldRoom = _rooms.FirstOrDefault(u => u.Key == room.Id).Value;
            _rooms.TryUpdate(oldRoom.Id, room, oldRoom);
        }

        public bool ExistOtherUserRoom(string idUser)
        {
            return _rooms.Select(p => p.Value)
                .Where(r => r.Owner != null)
                .Where(r => r.Owner.Id == idUser)
                .Count() > 0;
        }

        public bool IsUniqueRoomName(string name)
        {
            var users = _rooms
                .Select(x => x.Value)
                .Where(v => v.Name.ToUpper() == name.ToUpper())
                .ToList();
            return users.Count() == 0;
        }

    }
}
