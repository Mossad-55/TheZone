using TheZone.Domain.Common;
using TheZone.Domain.Enums;

namespace TheZone.Domain.Chats;

public class Message : BaseEntity
{
    public Guid ChatId { get; private set; }
    public Guid SenderId { get; private set; }
    public MessageType MessageType { get; private set; }
    public string? Content { get; private set; }
    public string? MediaUrl { get; private set; }
    public DateTimeOffset SentAt { get; private set; }

    private Message()
    {
        // Required by EF
    }
}