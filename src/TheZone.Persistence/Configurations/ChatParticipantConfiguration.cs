using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheZone.Domain.Chats;

namespace TheZone.Persistence.Configurations;

public sealed class ChatParticipantConfiguration : IEntityTypeConfiguration<ChatParticipant>
{
    public void Configure(EntityTypeBuilder<ChatParticipant> builder)
    {
        builder.ToTable("ChatParticipants");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Role)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.JoinedAt)
            .IsRequired();
        
        builder.Property(x => x.IsMuted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(x => new
        {
            x.ChatId,
            x.UserId
        })
        .IsUnique();

        builder.Property(x => x.ChatId)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired();
    }
}