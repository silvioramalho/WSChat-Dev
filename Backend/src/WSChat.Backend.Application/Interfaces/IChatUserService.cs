using System;
using System.Collections.Generic;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Interfaces
{
    public interface IChatUserService
    {
        public IList<ChatUser> GetAllUsers();

        public ChatUser CreateUser(string id, string nickname);

        public void AddUser(ChatUser user);

        public void RemoveUser(string id);

        public ChatUser GetUserById(string id);

        public string GetIdByName(string nickname);

        public bool IsUniqueNickname(string nickname);

        public void UpdateUser(ChatUser user);

        public IList<ChatUser> GetUsersInRoom(int idRoom);

        public bool IsUserInRoom(string idUser, int idRoom);

        public IList<ChatUser> GetUsersOutRooms();
    }
}
