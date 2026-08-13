using TheZone.Application.Abstractions.Messaging;

namespace TheZone.Application.Features.Messages.Commands.SendMessage;

public sealed record SendMessageCommand(Guid ChatId, Guid SenderId, string Content) : ICommand<Guid>;