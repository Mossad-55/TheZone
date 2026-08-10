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

    private Message(Guid chatId, Guid senderId, MessageType messageType, string? content, string? mediaUrl)
    {
        ChatId = chatId;
        SenderId = senderId;
        MessageType = messageType;
        Content = content;
        MediaUrl = mediaUrl;
        SentAt = DateTimeOffset.UtcNow;
    }

    public static Message Create(Guid chatId, Guid senderId, MessageType messageType, string? content, string? mediaUrl)
    {
        if(chatId == Guid.Empty)
        {
            throw new ArgumentException("Chat ID is required and cannot be empty.", nameof(chatId));
        }

        if(senderId == Guid.Empty)
        {
            throw new ArgumentException("Sender ID is required and cannot be empty.", nameof(senderId));
        }

        if(messageType == MessageType.Text)
        {
            if(string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Content is required for text messages.", nameof(content));
            }

            if(!string.IsNullOrEmpty(mediaUrl))
            {
                throw new ArgumentException("Media URL should be null or empty for text messages.", nameof(mediaUrl));
            }
        }
        else
        {
            if(string.IsNullOrWhiteSpace(mediaUrl))
            {
                throw new ArgumentException("Media URL is required for media messages.", nameof(mediaUrl));
            }
        }

        return new Message(chatId, senderId, messageType, content, mediaUrl);
    }
}