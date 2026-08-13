using TheZone.Application.Abstractions.Messaging;
using TheZone.Application.Common.Results;

namespace TheZone.Application.Features.Chats.Queries.GetUserChats;

public sealed record GetUserChatsQuery(Guid UserId) 
    : IQuery<List<ChatSummaryResponse>>;