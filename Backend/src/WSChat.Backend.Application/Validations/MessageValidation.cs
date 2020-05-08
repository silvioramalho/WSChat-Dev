using System;
using System.Collections.Generic;
using FluentValidation;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Validations
{
    public class MessageValidation : AbstractValidator<Message>
    {
        public MessageValidation()
        {
            RuleFor(expression: f => f.Event)
                .NotNull()
                .IsInEnum();



            #region .: Register User :.
            When(predicate: f => f.Event == EventEnum.RegisterUser, () =>
            {
                RuleFor(expression: f => f.MessageText)
                    .NotEmpty().WithMessage("The 'Nickname' must not be empty.")
                    .Length(min: 3, max: 20).WithMessage("The 'Nickname' must be between {MinLength} and {MaxLength} characters.")
                    .Matches(@"^[a-zA-Zá-úÁ-Ú0-9\s]+$").WithMessage("The 'Nickname' must contain only numbers and letters");

                RuleFor(expression: f => f.User)
                    .Null().WithMessage("You are already registered.");
            });
            #endregion

            #region .: Enter Room :.
            When(predicate: f => f.Event == EventEnum.EnterRoom, () =>
            {
                RuleFor(expression: f => f.User)
                    .NotNull().WithMessage("You need to register before using the chat");

                When(predicate: u => u.User != null, () =>
                {
                    RuleFor(expression: u => u.User.IdActiveRoom)
                        .Null().WithMessage("You are already in a room.");
                });

                RuleFor(expression: f => f.MessageText)
                    .NotEmpty().WithMessage("The 'Room Id' must not be empty.")
                    .Length(min: 1, max: 5).WithMessage("The 'Room Id' must be between {MinLength} and {MaxLength} characters.")
                    .Matches(@"^[0-9]+$").WithMessage("The 'Room Id' must contain only numbers");

            });
            #endregion

            #region .: Messaging :.
            When(predicate: f => f.Event == EventEnum.Messaging, () =>
            {
                RuleFor(expression: f => f.User)
                    .NotNull().WithMessage("You need to register before using the chat");

                When(predicate: u => u.User != null, () =>
                {
                    RuleFor(expression: u => u.User.IdActiveRoom)
                        .NotEmpty().WithMessage("You need to enter in some room to send messages.");
                });

                RuleFor(expression: f => f.MessageText)
                    .NotEmpty()
                    .MaximumLength(200);

                When(predicate: u => u.IsPrivate.HasValue && u.IsPrivate == true, () =>
                {
                    RuleFor(expression: u => u.TargetUserId)
                        .NotEmpty().WithMessage("You need to select a user to send message in private.");
                });
            });
            #endregion

            #region .:Exit Room :.
            When(predicate: f => f.Event == EventEnum.ExitRoom, () =>
            {
                RuleFor(expression: f => f.User)
                    .NotNull().WithMessage("You need to register before using the chat");

                When(predicate: u => u.User != null, () =>
                {
                    RuleFor(expression: u => u.User.IdActiveRoom.HasValue)
                        .Equal(true).WithMessage("You are not inside a room.");
                });

            });
            #endregion

            #region .: Create Room :.
            When(predicate: f => f.Event == EventEnum.CreateRoom, () =>
            {
                RuleFor(expression: f => f.User)
                    .NotNull().WithMessage("You need to register before using the chat");

                When(predicate: u => u.User != null, () =>
                {
                    RuleFor(expression: u => u.User.IdActiveRoom)
                        .Null().WithMessage("You cannot be inside a room to create another.");
                });

                RuleFor(expression: f => f.MessageText)
                .NotEmpty().WithMessage("The 'Room Name' must not be empty.")
                .Length(min: 3, max: 10).WithMessage("The 'Room Name' must be between {MinLength} and {MaxLength} characters.")
                .Matches(@"^[a-zA-Zá-úÁ-Ú0-9\s]+$").WithMessage("The 'Room Name' must contain only numbers and letters");
            });
            #endregion
        }

    }
}
