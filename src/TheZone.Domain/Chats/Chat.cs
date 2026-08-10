using TheZone.Domain.Common;
using TheZone.Domain.Enums;

namespace TheZone.Domain.Chats;

public class Chat : AuditableEntity
{
    public ChatType ChatType { get; private set; }
    public string? Name { get; private set; }
    public string? PictureUrl { get; private set; }
    private readonly List<ChatParticipant> _participants = new();
    private readonly List<Message> _messages = new();
    public IReadOnlyCollection<ChatParticipant> Participants => _participants;
    public IReadOnlyCollection<Message> Messages => _messages;

    private Chat()
    {
        // Required by EF
    }

    private Chat(ChatType chatType)
    {
        ChatType = chatType;
    }

    private Chat(ChatType chatType, string name, string? pictureUrl)
    {
        ChatType = chatType;
        Name = name;
        PictureUrl = pictureUrl;
    }

    public static Chat CreatePrivate()
    {
        return new Chat(ChatType.Private);
    }

    public static Chat CreateGroup(string name, string? pictureUrl)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Group name is required.", nameof(name));
        }

        return new Chat(ChatType.Group, name, pictureUrl);
    }

    public void AddParticipant(Guid userId, ParticipantRole role)
    {
        if(userId == Guid.Empty)
        {
            throw new ArgumentException("User ID is required and cannot be empty.", nameof(userId));
        }

        if (ChatType == ChatType.Private)
        {
            if (role != ParticipantRole.Member)
            {
                throw new InvalidOperationException("Private chats only support member participants.");
            }

            if (_participants.Count >= 2)
            {
                throw new InvalidOperationException("Private chats can only contain two participants.");
            }
        }

        if (_participants.Any(p => p.UserId == userId))
        {
            throw new InvalidOperationException("User is already a participant in the chat.");
        }

        var participant = ChatParticipant.Create(this.Id, userId, role);

        _participants.Add(participant);
    }

    public void AddMessage(Message message)
    {
        if(message == null)
        {
            throw new ArgumentNullException("Message cannot be null.", nameof(message));
        }

        _messages.Add(message);
    }
    public void UpdateGroupInfo(string name, string? pictureUrl)
    {
        if(this.ChatType != ChatType.Group)
        {
            throw new InvalidOperationException("Only group chats can be updated.");
        }

        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Group name is required.", nameof(name));
        }

        Name = name;
        PictureUrl = pictureUrl;
    }
}