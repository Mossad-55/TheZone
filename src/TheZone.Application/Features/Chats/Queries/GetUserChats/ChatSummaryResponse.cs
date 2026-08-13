namespace TheZone.Application.Features.Chats.Queries.GetUserChats;

public sealed record ChatSummaryResponse(
    Guid ChatId,
    string Name,
    string? ProfilePictureUrl,
    string? LastMessage,
    DateTimeOffset? LastMessageAt,
    int UnreadMessagesCount);