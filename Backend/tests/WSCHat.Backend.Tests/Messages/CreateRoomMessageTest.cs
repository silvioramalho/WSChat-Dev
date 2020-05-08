using NUnit.Framework;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Validations;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Messages
{
    [TestFixture]
    public class CreateRoomMessageTest
    {
        private MessageValidation validator;


        [SetUp]
        public void Setup()
        {

            validator = new MessageValidation();
        }

        #region .: Error :.

        [Test]
        public void Should_have_error_when_Event_is_null()
        {
            var message = CreateRoomMessageMockData.EventNull();
            validator.ShouldHaveValidationErrorFor(x => x.Event, message);
        }


        [Test]
        public void Should_have_error_when_User_is_outside_withou_roomname()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomWithoutRoomName();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_is_inside_without_roomname()
        {
            var message = CreateRoomMessageMockData.UserInsideRoomWithoutRoomName();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_outside_room_RoomName_have_2_chars()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomName2Chars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_outside_room_RoomName_have_11_chars()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomName11chars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_outside_room_RoomName_have_special_chars()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomNameSpecialChars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_inside_room_RoomName_ok()
        {
            var message = CreateRoomMessageMockData.UserInsideRoomWithRoomName();
            validator.ShouldHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_outside_room_RoomName_ok()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomNameOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.User, message);
        }

        [Test]
        public void Should_Not_have_error_when_User_outside_room_RoomName_ok_other_fields()
        {
            var message = CreateRoomMessageMockData.UserOutsideRoomRoomNameOKOtherFieldsOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }

        #endregion
    }
}