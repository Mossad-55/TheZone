using FluentValidation;

namespace TheZone.Application.Features.Chats.Commands.CreatePrivateChat;

public sealed class CreatePrivateChatCommandValidator : AbstractValidator<CreatePrivateChatCommand>
{
    public CreatePrivateChatCommandValidator()
    {
        RuleFor(x => x.CurrentUserId)
            .NotEmpty();

        RuleFor(x => x.TargetUserId)
            .NotEmpty();
        
        RuleFor(x => x)
            .Must(x => x.CurrentUserId != x.TargetUserId)
            .WithMessage("You cannot create a chat with yourself.");
    }
}