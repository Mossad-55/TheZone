using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheZone.Domain.Chats;

namespace TheZone.Persistence.Configurations;

public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ChatId)
            .IsRequired();

        builder.Property(x => x.SenderId)
            .IsRequired();
        
        builder.Property(x => x.MessageType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(1000);
        
        builder.Property(x => x.MediaUrl)
            .HasMaxLength(2048);
        
        builder.Property(x => x.SentAt)
            .IsRequired();
        
        builder.HasIndex(x => new
        {
            x.ChatId,
            x.SentAt
        });
    }
}