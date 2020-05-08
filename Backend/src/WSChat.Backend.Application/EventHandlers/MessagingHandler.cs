using System;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.EventHandlers
{
    public class MessagingHandler : BaseHandler
    {

        public MessagingHandler(
            IChatUserService userService,
            IRoomService roomService)
            : base(userService, roomService) {  }

        protected override Message HandleMessage(string socketId, Message message)
        {
            return message;
        }

        protected override void ValidateMessage(string socketId, Message message)
        {
            var targetUser = ValidateTargetUser(message.TargetUserId);
            ValidateTargetUserSession(message.User, targetUser);
        }


        private ChatUser ValidateTargetUser(string idTargetUser)
        {
            if (!String.IsNullOrEmpty(idTargetUser))
            {
                var user = GetUserById(idTargetUser);
                if (user == null)
                {
                    throw new Exception("Target User not found.");
                }
                return user;
            }
            return null;
            
        }

        private void ValidateTargetUserSession(ChatUser user, ChatUser targetUser)
        {
            if (targetUser != null)
            {
                if (user.IdActiveRoom != targetUser.IdActiveRoom)
                {
                    throw new Exception("User not found in this room.");
                }
            }
        }
    }
}
