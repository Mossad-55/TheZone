using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheZone.Domain.Chats;

namespace TheZone.Persistence.Configurations;

public sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ChatType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.PictureUrl)
            .HasMaxLength(2048);

        builder.HasMany(x => x.Participants)
            .WithOne()
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Messages)
            .WithOne()
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Participants)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Messages)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}