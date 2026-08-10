using TheZone.Domain.Common;
using TheZone.Domain.Enums;

namespace TheZone.Domain.Chats;

public class ChatParticipant : BaseEntity
{
    public Guid ChatId { get; private set; }
    public Guid UserId { get; private set; }
    public ParticipantRole Role { get; private set; }
    public DateTimeOffset JoinedAt { get; private set; }
    public Guid? LastReadMessageId { get; private set; }
    public bool IsMuted { get; private set; }

    private ChatParticipant()
    {
        // Required by EF
    }

    private ChatParticipant(Guid chatId, Guid userId, ParticipantRole role)
    {
        ChatId = chatId;
        UserId = userId;
        Role = role;
        JoinedAt = DateTimeOffset.UtcNow;
    }

    public static ChatParticipant Create(Guid chatId, Guid userId, ParticipantRole role)
    {
        if(chatId == Guid.Empty)
        {
            throw new ArgumentException("Chat ID is required and cannot be empty.", nameof(chatId));
        }

        if(userId == Guid.Empty)
        {
            throw new ArgumentException("User ID is required and cannot be empty.", nameof(userId));
        }
        
        return new ChatParticipant(chatId, userId, role);
    }
}