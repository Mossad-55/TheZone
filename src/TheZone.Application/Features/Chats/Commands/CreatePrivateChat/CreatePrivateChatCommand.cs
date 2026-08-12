
using TheZone.Application.Abstractions.Messaging;

namespace TheZone.Application.Features.Chats.Commands.CreatePrivateChat;

public sealed record CreatePrivateChatCommand(Guid CurrentUserId, Guid TargetUserId) : ICommand<Guid>;