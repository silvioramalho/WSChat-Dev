using NUnit.Framework;
using WSChat.Backend.Application.Extensions;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Application.Services;
using WSChat.Backend.Domain.Enums;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Services
{
    [TestFixture]
    public class CreateRoomServiceTest
    {
        private IMessageService _messageService;
        private IRoomService _roomService;
        private IChatUserService _userService;
        private string socketId = "a6cf4f3b80f748a69cd1463eea292c9a";
        


        [SetUp]
        public void Setup()
        {
            _roomService =  new RoomService();
            _userService = new ChatUserService();
            _messageService = new MessageService(_userService, _roomService);
        }

        #region .: Error :.

        [Test]
        public void Should_have_error_when_Event_is_null()
        {
            var message = CreateRoomMessageMockData.EventNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }


        [Test]
        public void Should_have_error_when_User_is_outside_withou_roomname()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomWithoutRoomName().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_is_inside_without_roomname()
        {
            var message = CreateRoomMessageMockData.UserInsideRoomWithoutRoomName().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_outside_room_RoomName_have_2_chars()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomName2Chars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_outside_room_RoomName_have_11_chars()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomName11chars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_outside_room_RoomName_have_special_chars()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomNameSpecialChars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_inside_room_RoomName_ok()
        {
            var message = CreateRoomMessageMockData.UserInsideRoomWithRoomName().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_outside_room_RoomName_ok()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomNameOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_Not_have_error_when_User_outside_room_RoomName_ok_other_fields()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomNameOKOtherFieldsOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion
    }

}