using Microsoft.EntityFrameworkCore;
using TheZone.Application.Abstractions.Data;
using TheZone.Application.Abstractions.Messaging;
using TheZone.Application.Common.Results;
using TheZone.Application.Features.Chats.Errors;
using TheZone.Domain.Chats;
using TheZone.Domain.Enums;

namespace TheZone.Application.Features.Messages.Commands.SendMessage;

public sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public SendMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<Guid>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(
            x => x.Id == request.ChatId,
            cancellationToken);

        if (chat is null)
        {
            return Result<Guid>.Failure(ChatErrors.ChatNotFound);
        }

        var isParticipant = await _context.ChatParticipants.AnyAsync(
            cp => cp.ChatId == request.ChatId &&
            cp.UserId == request.SenderId,
            cancellationToken);

        if(!isParticipant)
        {
            return Result<Guid>.Failure(ChatErrors.NotParticipant);
        }

        var message = chat.AddMessage(
            request.SenderId,
            MessageType.Text,
            request.Content,
            null);

        _context.Messages.Add(message);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(message.Id);
    }
}