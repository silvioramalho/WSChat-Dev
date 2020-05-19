using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSChat.Backend.Application.DTO;
using WSChat.Backend.Application.EventHandlers;
using WSChat.Backend.Application.Extensions;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IChatUserService _userService;
        private readonly IRoomService _roomService;

        public MessageService(
            IChatUserService userService,
            IRoomService roomService)
        {
            _userService = userService;
            _roomService = roomService;
        }

        #region .: Handle Request :.

        public IList<ChatPayloadDTO> HandleRequest(string userId, string message)
        {
            IList<ChatPayloadDTO> result = new List<ChatPayloadDTO>();

            if(!message.IsValidJsonPayload())
            {
                Message errorMessage = new Message();
                errorMessage.Event = EventEnum.Error;
                errorMessage.MessageText = "Invalid request.";

                result.Add(errorMessage.MessageToPayload(userId));
                return result;
            }

            Message objMessage = message.DeserializeMessage();

            if (objMessage.Event == 0 || objMessage.Event == EventEnum.Error)
            {
                objMessage.Event = EventEnum.Error;
                objMessage.MessageText = "Invalid request.";
                result.Add(objMessage.MessageToPayload(userId));
                return result;
            }
            

            switch (objMessage.Event)
            {
                case EventEnum.SocketConnect:
                    break;
                case EventEnum.RegisterUser:
                    result = ProcessRequestRegisterUser(userId, objMessage);
                    break;
                case EventEnum.EnterRoom:
                    result = ProcessRequestEnterRoom(userId, objMessage);
                    break;
                case EventEnum.Messaging:
                    result = ProcessRequestMessaging(userId, objMessage);
                    break;
                case EventEnum.ExitRoom:
                    result = ProcessRequestExitRoom(userId, objMessage);
                    break;
                case EventEnum.CreateRoom:
                    result = ProcessRequestCreateRoom(userId, objMessage);
                    break;
                case EventEnum.UpdateUserList:
                case EventEnum.UpdateRoomList:
                case EventEnum.WelcomeMessage:
                case EventEnum.GoodbyeMessage:
                case EventEnum.SocketDisconnect:
                case EventEnum.UpdateUsersRooms:
                case EventEnum.Error:
                default:
                    break;
            }

            return result;
        }

        private IList<ChatPayloadDTO> ProcessRequestRegisterUser(string userId, Message message)
        {
            IList<ChatPayloadDTO> result = new List<ChatPayloadDTO>();

            message = new RegisterUserHandler(_userService)
                .Handle(userId, message);

            if (message.Event != EventEnum.Error)
            {
                message.AvailableRooms = GetAllRooms();
                message.User = GetUserById(userId);
            }

            result.Add(message.MessageToPayload(userId));
            return result;
        }

        private IList<ChatPayloadDTO> ProcessRequestEnterRoom(string userId, Message message)
        {
            IList<ChatPayloadDTO> result = new List<ChatPayloadDTO>();

            message.User = GetUserById(userId);

            message = new EnterRoomHandler(_userService, _roomService)
                .Handle(userId, message);
            result.Add(message.MessageToPayload(userId));

            if (message.Event != EventEnum.Error)
            {
                Message newMessage = message.GenerateEventMessage(EventEnum.UpdateUserList);
                result.Add(newMessage.MessageToPayload(userId));

                newMessage = message.GenerateEventMessage(EventEnum.UpdateUsersRooms);
                newMessage.AvailableRooms = GetAllRooms();
                result.Add(newMessage.MessageToPayload(userId));
            }
            
            return result;
        }

        private IList<ChatPayloadDTO> ProcessRequestMessaging(string userId, Message message)
        {
            IList<ChatPayloadDTO> result = new List<ChatPayloadDTO>();

            message.User = GetUserById(userId);

            message = new MessagingHandler(_userService, _roomService)
                .Handle(userId, message);

            // Composing message with target user informations
            if (message.Event != EventEnum.Error
                && !String.IsNullOrWhiteSpace(message.TargetUserId))
            {
                message.TargetUser = GetUserById(message.TargetUserId);
            }

            result.Add(message.MessageToPayload(userId));

            return result;
        }

        private IList<ChatPayloadDTO> ProcessRequestExitRoom(string userId, Message message)
        {
            IList<ChatPayloadDTO> result = new List<ChatPayloadDTO>();

            message.User = GetUserById(userId);
            int roomId = 0;
            if (message.User != null && message.User.IdActiveRoom.HasValue)
                roomId = message.User.IdActiveRoom.Value;



            message = new ExitRoomHandler(_userService, _roomService)
                .Handle(userId, message);

            result.Add(message.MessageToPayload(userId));

            if (message.Event != EventEnum.Error && roomId != 0)
            {
                Message newMessage = message.GenerateEventMessage(EventEnum.UpdateUserList);
                newMessage = AddRoomInMessage(roomId, newMessage);
                result.Add(newMessage.MessageToPayload(userId));

                newMessage = message.GenerateEventMessage(EventEnum.UpdateUsersRooms);
                newMessage.AvailableRooms = GetAllRooms();
                result.Add(newMessage.MessageToPayload(userId));
            }

            return result;
        }

        private IList<ChatPayloadDTO> ProcessRequestCreateRoom(string userId, Message message)
        {
            IList<ChatPayloadDTO> result = new List<ChatPayloadDTO>();

            message.User = GetUserById(userId);

            message = new CreateRoomHandler(_userService, _roomService)
                .Handle(userId, message);

            result.Add(message.MessageToPayload(userId));

            if (message.Event != EventEnum.Error)
            {
                Message newMessage = message.GenerateEventMessage(EventEnum.UpdateRoomList);
                newMessage.AvailableRooms = GetAllRooms();
                result.Add(newMessage.MessageToPayload(userId));
            }

            return result;
        }

        private Message AddRoomInMessage(int roomId, Message message)
        {
            message.Room = GetRoomById(roomId);
            message.Room.Users = GetUsersInRoom(roomId);
            return message;

        }

        #endregion

        #region .: Room Services :.

        private IList<Room> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }

        #endregion

        #region .: User Services :.

        private ChatUser GetUserById(string id)
        {
            return _userService.GetUserById(id);
        }

        private Room GetRoomById(int roomId)
        {
            return _roomService.GetRoomById(roomId);
        }

        private IList<ChatUser> GetUsersInRoom(int roomId)
        {
            return _userService.GetUsersInRoom(roomId);
        }

        private void RemoveUser(string userId)
        {
            _userService.RemoveUser(userId);
        }

        #endregion

        #region .: Handle Disconnection :.

        public ChatPayloadDTO HandleDisconnection(string userId)
        {

            var user = GetUserById(userId);
            Message message = new Message();
            message.Event = EventEnum.SocketDisconnect;
            message.User = user;
            
            if (user != null)
            {
                RemoveUser(userId);
                if (user.IdActiveRoom.HasValue)
                {
                    message = message.GenerateEventMessage(EventEnum.UpdateUserList);
                    message = AddRoomInMessage(user.IdActiveRoom.Value, message);
                }
                return message.MessageToPayload(userId);
            }
            
            return message.MessageToPayload(userId);
        }

        #endregion


    }
}