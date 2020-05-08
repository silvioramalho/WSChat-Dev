using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Services
{
    public class ChatUserService : IChatUserService
    {
        private ConcurrentDictionary<string, ChatUser> _users = new ConcurrentDictionary<string, ChatUser>();

        public IList<ChatUser> GetAllUsers()
        {
            return _users.Select(p => p.Value).ToList();
        }

        public ChatUser CreateUser(string id, string nickname)
        {
            return new ChatUser(id, nickname);
        }

        public void AddUser(ChatUser user)
        {
            _users.TryAdd(user.Id, user);
        }

        public void RemoveUser(string id)
        {
            _users.TryRemove(id, out var user);
        }

        public ChatUser GetUserById(string id)
        {
            return _users.FirstOrDefault(x => x.Key == id).Value;
        }

        public string GetIdByName(string nickname)
        {
            return _users.FirstOrDefault(x => x.Value.Nickname == nickname).Key;
        }

        public bool IsUniqueNickname(string nickname)
        {
            var users = _users
                .Select(x => x.Value)
                .Where(v => v.Nickname.ToUpper() == nickname.ToUpper())
                .ToList();
            return users.Count() == 0;
        }

        public void UpdateUser(ChatUser user)
        {
            ChatUser oldUser = _users.FirstOrDefault(u => u.Key == user.Id).Value;
            _users.TryUpdate(oldUser.Id, user, oldUser);
        }

        public IList<ChatUser> GetUsersInRoom(int idRoom)
        {
            return _users.Select(u => u.Value)
                .Where(v => v.IdActiveRoom.HasValue)
                .Where(v => v.IdActiveRoom.Value == idRoom)
                .ToList();
        }

        public IList<ChatUser> GetUsersOutRooms()
        {
            return _users.Select(u => u.Value)
                .Where(v => !v.IdActiveRoom.HasValue)
                .ToList();
        }

        public bool IsUserInRoom(string idUser, int idRoom)
        {
            return GetUserById(idUser).IdActiveRoom == idRoom;
        }
    }
}
