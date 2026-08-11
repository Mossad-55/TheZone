using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheZone.Domain.Users;

namespace TheZone.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();
        
        builder.Property(x => x.DisplayName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(x => x.DisplayName);

        builder.Property(x => x.Email)
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Bio)
            .HasMaxLength(500);

        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(2048);
    }
}