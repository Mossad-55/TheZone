using TheZone.Application.Common.Results;

namespace TheZone.Application.Features.Chats.Errors;

public static class ChatErrors
{
    public static readonly Error AlreadyExists = 
        new(
            "Chat.AlreadyExists",
            "Private chat already exists."
        );
    
    public static readonly Error ParticipantNotFound =
        new(
            "Chat.ParticipantNotFound",
            "One or more participants were not found."
        );
    
    public static readonly Error ChatNotFound = 
        new(
            "Chat.NotFound",
            "Chat was not found."
        );

    public static readonly Error NotParticipant = 
        new(
            "Chat.NotParticipant",
            "User is not a participant in this chat."
        );
}