using NUnit.Framework;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Validations;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Messages
{
    [TestFixture]
    public class MessagingMessageTest
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
            var message = MessagingMessageMockData.EventNull();
            validator.ShouldHaveValidationErrorFor(x => x.Event, message);
        }

        [Test]
        public void Should_have_error_when_Message_with_user_is_null()
        {
            var message = MessagingMessageMockData.MessageWithouUser();
            validator.ShouldHaveValidationErrorFor(x => x.User, message);
        }


        [Test]
        public void Should_have_error_when_User_outside_room()
        {
            var message = MessagingMessageMockData.UserOutsideRoomWithouMessage();
            validator.ShouldHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        [Test]
        public void Should_have_error_when_User_outside_room_with_message()
        {
            var message = MessagingMessageMockData.UserOutsideRoomWithMessage();
            validator.ShouldHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        [Test]
        public void Should_have_error_when_user_inside_room_without_message()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithoutMessage();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_user_inside_room_message_201_chars()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessage201Chars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message_numbers()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageNumbersOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }
        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message_1_char()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessage1CharOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_Not_have_error_when_User_inside_room_with_message_special_chars()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageSpecialCharOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_Not_have_error_when_User_inside_room_other_fields()
        {
            var message = MessagingMessageMockData.UserInsideRoomWithMessageOtherFieldsOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }

        #endregion
    }
}