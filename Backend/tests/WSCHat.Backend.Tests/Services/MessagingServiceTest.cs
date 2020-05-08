using NUnit.Framework;
using WSChat.Backend.Application.Extensions;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Application.Services;
using WSChat.Backend.Domain.Enums;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Services
{
    [TestFixture]
    public class MessagingServiceTest
    {
        private IMessageService _messageService;
        private IRoomService _roomService;
        private IChatUserService _userService;
        private string socketId = "a6cf4f3b80f748a69cd1463eea292c9a";


        [SetUp]
        public void Setup()
        {

            _roomService = new RoomService();
            _userService = new ChatUserService();
            _messageService = new MessageService(_userService, _roomService);
        }

        #region .: Error :.

        [Test]
        public void Should_have_error_when_Event_is_null()
        {
            var message = MessagingMessageMockData.EventNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_Message_with_user_is_null()
        {
            var message = MessagingMessageMockData.MessageWithouUser().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }


        [Test]
        public void Should_have_error_when_User_outside_room()
        {
            var message = MessagingMessageMockData.UserOutsideRoomWithouMessage().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_outside_room_with_message()
        {
            var message = MessagingMessageMockData.UserOutsideRoomWithMessage().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_user_inside_room_without_message()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithoutMessage().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_user_inside_room_message_201_chars()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessage201Chars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message_numbers()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageNumbersOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }
        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message_1_char()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessage1CharOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message_special_chars()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageSpecialCharOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_Not_have_error_when_User_inside_room_other_fields()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageOtherFieldsOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion
    }
}