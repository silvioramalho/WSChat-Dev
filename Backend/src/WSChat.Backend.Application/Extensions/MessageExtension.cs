using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WSChat.Backend.Application.DTO;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Extensions
{
    public static class MessageExtension
    {

        private static DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        public static string SerializeMessage(this Message message)
        {
            return JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static Message DeserializeMessage(this string message)
        {
            try
            {
                return JsonConvert.DeserializeObject<Message>(message);
            }
            catch
            {
                Message errorMessage = new Message();
                errorMessage.Event = EventEnum.Error;
                return errorMessage;
            }
            
        }

        public static bool IsValidJsonPayload(this string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) ||
                (strInput.StartsWith("[") && strInput.EndsWith("]")))
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;

            }
        }

        public static ChatPayloadDTO MessageToPayload(this Message message, string userId)
        {
            if (message.User == null)
                return new ChatPayloadDTO()
                {
                    Event = message.Event,
                    Message = message.SerializeMessage(),
                    UserId = userId
                };
            else
                return new ChatPayloadDTO()
                {
                    Event = message.Event,
                    IsPrivate = message.IsPrivate,
                    TargetUserId = message.TargetUserId,
                    RoomId = message.User.IdActiveRoom.HasValue ? message.User?.IdActiveRoom : message.Room?.Id,
                    Message = message.SerializeMessage(),
                    UserId = userId
                };
        }
    }
}
