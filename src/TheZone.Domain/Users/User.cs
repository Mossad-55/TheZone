using TheZone.Domain.Common;

namespace TheZone.Domain.Users;

public class User : AuditableEntity
{
    public string PhoneNumber { get; private set; } = null!;
    public string DisplayName { get; private set; } = null!;
    public string? Bio { get; private set; }
    public string? Email { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public DateTimeOffset? LastSeenAt { get; private set; }
    
    private User()
    {
        // Required by EF
    }

    private User(string phoneNumber, string displayName)
    {
        PhoneNumber = phoneNumber.Trim();
        DisplayName = displayName.Trim();
    }

    public static User Create(string phoneNumber, string displayName)
    {
        if(string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required and cannot be null or empty.", nameof(phoneNumber));

        if(string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Display name is required and cannot be null or empty.", nameof(displayName));

        return new User(phoneNumber, displayName);
    }

    public void UpdateProfile(string displayName, string? bio, string? email, string? profilePictureUrl)
    {
        if(string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Display name is required and cannot be null or empty.", nameof(displayName));

        DisplayName = displayName.Trim();
        Bio = bio;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;

        MarkAsUpdated();
    }

    public void UpdateLastSeen()
    {
        LastSeenAt = DateTimeOffset.UtcNow;

        MarkAsUpdated();
    }
}