using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSChat.Backend.Application.DTO;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.API.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Lib.ChatWebSocket;

namespace WSChat.Backend.API.Handlers
{
    public class ChatHandler : WebSocketHandler, IMessageChat
    {
        private readonly IMessageService _messageService;
        private readonly IChatUserService _userService;

        public ChatHandler(ConnectionService connections) : base(connections) { }

        public ChatHandler(ConnectionService connections,
            IMessageService messageService,
            IChatUserService userService) : base(connections)
        {
            _messageService = messageService;
            _userService = userService;
        }

        #region .: Handle Connections :.

        public override async Task OnConnected(IDisposable socket)
        {
            await base.OnConnected(socket);

            var socketId = Connections.GetId(socket);
            await SendMessage(socketId, $"{{\"idConnection\":\"{socketId}\"}}");
        }

        public override async Task OnDisconnected(IDisposable socket)
        {
            var socketId = Connections.GetId(socket);
            await base.OnDisconnected(socket);

            var payload = HandleDisconnection(socketId);
            await HandleResponse(payload);
        }

        public ChatPayloadDTO HandleDisconnection(string userId)
        {
            return _messageService.HandleDisconnection(userId);
        }

        #endregion

        #region .: Handle Request :.

        public override async Task Receive(string socketId, int length, byte[] buffer)
        {
            foreach (var payload in HandleRequest(socketId, Encoding.UTF8.GetString(buffer, 0, length)))
            {
                await HandleResponse(payload);
            }
        }

        public IList<ChatPayloadDTO> HandleRequest(string socketId, string message)
        {
            return _messageService.HandleRequest(socketId, message);
        }

        #endregion

        #region .: Handle Response :.

        public async Task HandleResponse(ChatPayloadDTO payload)
        {

            switch (payload.Event)
            {
                case EventEnum.RegisterUser:
                    await SendMessage(payload.UserId, payload.Message);
                    break;
                case EventEnum.Messaging:
                    if (payload.IsPrivate.HasValue && payload.IsPrivate.Value == true &&
                        !String.IsNullOrWhiteSpace(payload.TargetUserId))
                    {
                        await SendPrivateMessage(payload);
                    }
                    else
                    {
                        await SendRoomMessageToAll(payload);
                    }

                    break;
                case EventEnum.EnterRoom:
                    await SendMessage(payload.UserId, payload.Message);
                    break;
                case EventEnum.ExitRoom:
                    await SendMessage(payload.UserId, payload.Message);
                    break;
                case EventEnum.CreateRoom:
                    await SendMessageToAllOutsideRoom(payload);
                    break;
                case EventEnum.UpdateUserList:
                    await SendRoomMessageToAll(payload);
                    break;
                case EventEnum.UpdateRoomList:
                    await SendMessageToAllOutsideRoom(payload);
                    break;
                case EventEnum.WelcomeMessage:
                    await SendMessage(payload.UserId, payload.Message);
                    break;
                case EventEnum.GoodbyeMessage:
                    await SendMessage(payload.UserId, payload.Message);
                    break;
                case EventEnum.SocketDisconnect:
                    break;
                case EventEnum.UpdateUsersRooms:
                    await SendMessageToAllOutsideRoom(payload);
                    break;
                case EventEnum.Error:
                    await SendMessage(payload.UserId, payload.Message);
                    break;
                default:
                    break;
            }

        }

        #endregion

        #region .: Send Data :.

        public async Task SendMessageToAllUsers(ChatPayloadDTO payload)
        {
            foreach (var user in _userService.GetAllUsers())
            {
                await SendMessage(user.Id, payload.Message);
            }
        }


        public async Task SendRoomMessageToAll(ChatPayloadDTO payload)
        {
            if (payload.RoomId.HasValue)
            {
                foreach (var user in _userService.GetUsersInRoom(payload.RoomId.Value))
                {
                    if (user.Id.Equals(payload.UserId))
                    {
                        if (AllowSendToOrignUser(payload.Event))
                            await SendMessage(user.Id, payload.Message);
                    }
                    else
                    {
                        await SendMessage(user.Id, payload.Message);
                    }

                }
            }
        }

        public async Task SendPrivateMessage(ChatPayloadDTO payload)
        {
            if (_userService.IsUserInRoom(payload.TargetUserId, payload.RoomId.Value))
            {
                await SendMessage(payload.UserId, payload.Message);
                await SendMessage(payload.TargetUserId, payload.Message);
            }
            else
            {
                await SendMessage(payload.UserId, payload.Message);
            }
        }

        public async Task SendMessageToAllOutsideRoom(ChatPayloadDTO payload)
        {
            foreach (var user in _userService.GetUsersOutRooms())
            {
                await SendMessage(user.Id, payload.Message);
            }
        }

        private bool AllowSendToOrignUser(EventEnum payloadEvent)
        {
            return payloadEvent != EventEnum.UpdateUserList &&
                payloadEvent != EventEnum.UpdateRoomList;
        }

        #endregion

    }
}