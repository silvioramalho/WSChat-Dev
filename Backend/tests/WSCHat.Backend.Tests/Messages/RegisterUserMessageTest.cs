using NUnit.Framework;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Validations;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Messages
{
    [TestFixture]
    public class RegisterUserMessageTest
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
            var message = RegisterUserMessageMockData.EventNull();
            validator.ShouldHaveValidationErrorFor(x => x.Event, message);
        }


        [Test]
        public void Should_have_error_when_Nickname_is_null()
        {
            var message = RegisterUserMessageMockData.NicknameNull();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_Nickname_have_2_chars()
        {
            var message = RegisterUserMessageMockData.Nickname2Chars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_Nickname_have_21_chars()
        {
            var message = RegisterUserMessageMockData.Nickname21chars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_Nickname_have_special_chars()
        {
            var message = RegisterUserMessageMockData.NicknameSpecialChars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_is_registred()
        {
            var message = RegisterUserMessageMockData.UserRegistered();
            validator.ShouldHaveValidationErrorFor(x => x.User, message);
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_not_have_error_when_Nickname_is_specified()
        {
            var message = RegisterUserMessageMockData.NicknameOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }

        #endregion

    }
}