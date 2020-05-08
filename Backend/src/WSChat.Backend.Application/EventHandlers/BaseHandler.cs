using System;
using System.Collections.Generic;
using System.Linq;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Application.Validations;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.EventHandlers
{
    public abstract class BaseHandler : IChatUserService
    {
        protected readonly IChatUserService _userService;
        private readonly IRoomService _roomService;

        public BaseHandler(
               IChatUserService userService
            )
        {
            _userService = userService;
        }

        public BaseHandler(
               IChatUserService userService,
               IRoomService roomService
            )
        {
            _userService = userService;
            _roomService = roomService;
        }

        public Message Handle(string socketId, Message message)
        {
            try
            {
                Validate(socketId, message);
                message = HandleMessage(socketId, message);
            }
            catch (Exception ex)
            {
                message.Event = EventEnum.Error;
                message.MessageText = ex.Message;
            }
            
            return message;
        }

        protected virtual void Validate(string socketId, Message message)
        {
            var validator = new MessageValidation().Validate(message);
            if (!validator.IsValid) throw new Exception(validator.Errors.FirstOrDefault().ErrorMessage);

            ValidateMessage(socketId, message);
        }

        protected abstract void ValidateMessage(string socketId, Message message);
        protected abstract Message HandleMessage(string socketId, Message message);

        public IList<ChatUser> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        public ChatUser CreateUser(string id, string nickname)
        {
            return _userService.CreateUser(id, nickname);
        }

        public void AddUser(ChatUser user)
        {
            _userService.AddUser(user);
        }

        public void RemoveUser(string id)
        {
            _userService.RemoveUser(id);
        }

        public ChatUser GetUserById(string id)
        {
            return _userService.GetUserById(id);
        }

        public string GetIdByName(string nickname)
        {
            return _userService.GetIdByName(nickname);
        }

        public bool IsUniqueNickname(string nickname)
        {
            return _userService.IsUniqueNickname(nickname);
        }

        public void UpdateUser(ChatUser user)
        {
            _userService.UpdateUser(user);
        }

        public IList<ChatUser> GetUsersInRoom(int IdRoom)
        {
            return _userService.GetUsersInRoom(IdRoom);
        }

        public bool IsUserInRoom(string idUser, int idRoom)
        {
            return _userService.IsUserInRoom(idUser, idRoom);
        }
        public IList<ChatUser> GetUsersOutRooms()
        {
            return _userService.GetUsersOutRooms();
        }

        public Room GetRoomById(int idRoom)
        {
            return _roomService.GetRoomById(idRoom);
        }

        public IList<Room> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }

        public bool ExistOtherUserRoom(string idUser)
        {
            return _roomService.ExistOtherUserRoom(idUser);
        }

        public int CreateRoom(string name)
        {
            return _roomService.CreateRoom(name);
        }

        public void UpdateRoom(Room room)
        {
            _roomService.UpdateRoom(room);
        }

        public bool IsUniqueRoomName(string name)
        {
            return _roomService.IsUniqueRoomName(name);
        }
    }
}
