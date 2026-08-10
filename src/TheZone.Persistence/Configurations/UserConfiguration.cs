using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheZone.Domain.Users;

namespace TheZone.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();
        
        builder.Property(u => u.DisplayName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(u => u.DisplayName);

        builder.Property(u => u.Email)
            .HasMaxLength(255);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Bio)
            .HasMaxLength(500);

        builder.Property(u => u.ProfilePictureUrl)
            .HasMaxLength(2048); // Maximum URL length

        builder.Property(u => u.CreatedAt)
            .IsRequired();
    }
}