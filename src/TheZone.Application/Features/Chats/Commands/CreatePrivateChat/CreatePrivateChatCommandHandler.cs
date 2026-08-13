using Microsoft.EntityFrameworkCore;
using TheZone.Application.Abstractions.Data;
using TheZone.Application.Abstractions.Messaging;
using TheZone.Application.Common.Results;
using TheZone.Application.Features.Chats.Errors;
using TheZone.Domain.Chats;
using TheZone.Domain.Enums;

namespace TheZone.Application.Features.Chats.Commands.CreatePrivateChat;

public sealed class CreatePrivateChatCommandHandler
    : ICommandHandler<CreatePrivateChatCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePrivateChatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreatePrivateChatCommand request,
        CancellationToken cancellationToken)
    {
        var userCount = await _context.Users.CountAsync(
            x => x.Id == request.CurrentUserId || 
            x.Id == request.TargetUserId,
            cancellationToken);
        
        if(userCount != 2)
        {
            return Result<Guid>.Failure(ChatErrors.ParticipantNotFound);
        }

        var chatExists = await _context.Chats
            .AnyAsync(
                chat =>
                    chat.ChatType == ChatType.Private
                    &&
                    chat.Participants.Any(
                        p => p.UserId == request.CurrentUserId)
                    &&
                    chat.Participants.Any(
                        p => p.UserId == request.TargetUserId),
                cancellationToken);

        if (chatExists)
        {
            return Result<Guid>.Failure(ChatErrors.AlreadyExists);
        }

        var chat = Chat.CreatePrivate();

        chat.AddParticipant(
            request.CurrentUserId,
            ParticipantRole.Member);

        chat.AddParticipant(
            request.TargetUserId,
            ParticipantRole.Member);
        
        _context.Chats.Add(chat);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(chat.Id);
    }
}