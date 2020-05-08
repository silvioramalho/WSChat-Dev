using System;
using System.Collections.Generic;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.API.Interfaces
{
    public interface IChatUser
    {
        public IList<ChatUser> GetAllUsers();

        public void AddUser(string id, string nickname);

        public void RemoveUser(string id);

        public ChatUser GetUserById(string id);

        public string GetIdByName(string nickname);

        public bool IsUniqueNickname(string nickname);
    }
}
