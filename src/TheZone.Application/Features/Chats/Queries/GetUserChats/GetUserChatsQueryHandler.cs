using Microsoft.EntityFrameworkCore;
using TheZone.Application.Abstractions.Data;
using TheZone.Application.Abstractions.Messaging;
using TheZone.Application.Common.Results;
using TheZone.Application.Features.Chats.Errors;
using TheZone.Domain.Chats;

namespace TheZone.Application.Features.Chats.Queries.GetUserChats;

public sealed class GetUserChatsQueryHandler : IQueryHandler<GetUserChatsQuery, List<ChatSummaryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetUserChatsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<ChatSummaryResponse>>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _context.Users.AnyAsync(
        u => u.Id == request.UserId,
        cancellationToken);

        if (!userExists)
        {
            return Result<List<ChatSummaryResponse>>
                .Failure(ChatErrors.ParticipantNotFound);
        }

        var chatIds = await _context.ChatParticipants
            .Where(cp => cp.UserId == request.UserId)
            .Select(cp => cp.ChatId)
            .ToListAsync(cancellationToken);

        var chats = await _context.Chats
            .Where(c => chatIds.Contains(c.Id))
            .Select(chat => new ChatSummaryResponse(
                chat.Id,
                chat.Name ?? string.Empty,
                chat.PictureUrl,
                chat.Messages
                    .OrderByDescending(m => m.SentAt)
                    .Select(m => m.Content)
                    .FirstOrDefault(),
                chat.Messages
                    .OrderByDescending(m => m.SentAt)
                    .Select(m => (DateTimeOffset?)m.SentAt)
                    .FirstOrDefault(),
            0))
            .ToListAsync(cancellationToken);

        return Result<List<ChatSummaryResponse>>
            .Success(chats);
    }
}