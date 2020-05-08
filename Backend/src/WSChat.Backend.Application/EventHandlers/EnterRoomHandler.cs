using System;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.EventHandlers
{
    public class EnterRoomHandler : BaseHandler
    {
        public EnterRoomHandler(
            IChatUserService userService,
            IRoomService roomService)
            : base(userService, roomService) { }


        protected override Message HandleMessage(string socketId, Message message)
        {

            int idRoom = Convert.ToInt32(message.MessageText);
            
            message.User.IdActiveRoom = idRoom;
            UpdateUser(message.User);

            var room = GetRoomById(idRoom);
            room.Users = GetUsersInRoom(idRoom);
            message.Room = room;

            message.Event = EventEnum.WelcomeMessage;
            message.MessageText = $"Welcome {message.User.Nickname} to the #{room.Name} room.";
           
            return message;
        }

        protected override void ValidateMessage(string socketId, Message message)
        {
            //IsUserInSomeRoom(message.User);
            int idRoom = ValidateRoomId(message.MessageText);
            RoomSessionNotExist(idRoom);
        }

        private int ValidateRoomId(string message)
        {
            if (!Int32.TryParse(message, out int idRoom))
            {
                throw new Exception("Invalid room ID.");
            }

            return idRoom;
        }

        private void RoomSessionNotExist(int idRoom)
        {
            if (GetRoomById(idRoom) == null)
            {
                throw new Exception("The room is closed or not exist.");
            }
        }

        //private void IsUserInSomeRoom(ChatUser user)
        //{
        //    if (user.IdActiveRoom.HasValue)
        //    {
        //        throw new Exception("You are already in a room.");
        //    }
        //}

        
    }
}