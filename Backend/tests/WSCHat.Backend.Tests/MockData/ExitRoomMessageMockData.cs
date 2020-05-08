using System;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSCHat.Backend.Tests.MockData
{
    /// <summary>
    /// Testing Message when Event is Exit Room.
    /// Required Fields:
    /// - Event
    /// - User (IdActiveRoom not null)
    /// </summary>
    public static class ExitRoomMessageMockData
    {
        private static readonly EventEnum eventTest = EventEnum.ExitRoom;

        #region .: Errors :.

        public static Message EventNull()
        {
            return new Message();
        }

        public static Message EventOK()
        {
            var message = new Message();
            message.Event = eventTest;
            return message;
        }

        public static Message UserOutsideRoom()
        {
            var message = EventOK();
            message.User = new ChatUser(Guid.NewGuid().ToString("N"), "Mary");

            return message;
        }

        #endregion

        #region .: Success :.

        public static Message UserInsideRoomOK()
        {
            var message = UserOutsideRoom();
            message.User.IdActiveRoom = 1;

            return message;
        }

        public static Message UserInsideRoomWithMessageTextOK()
        {
            var message = UserInsideRoomOK();
            message.MessageText = "whatever 123 @*&ˆ$&#";

            return message;
        }

        public static Message UserInsideRoomWithOtherFieldsOK()
        {
            var message = UserInsideRoomWithMessageTextOK();
            message.IsPrivate = true;
            message.TargetUserId = "any";

            return message;
        }

        #endregion


    }
}
