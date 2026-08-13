using FluentValidation;

namespace TheZone.Application.Features.Messages.Commands.SendMessage;

public sealed class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty();

        RuleFor(x => x.SenderId)
            .NotEmpty();
        
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Message content cannot exceed 1000 characters.");
    }
} 