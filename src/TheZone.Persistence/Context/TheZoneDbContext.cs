using Microsoft.EntityFrameworkCore;
using TheZone.Application.Abstractions.Data;
using TheZone.Domain.Chats;
using TheZone.Domain.Users;

namespace TheZone.Persistence.Context;

public sealed class TheZoneDbContext : DbContext, IApplicationDbContext
{
    public TheZoneDbContext(DbContextOptions<TheZoneDbContext> options) 
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheZoneDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();
    public DbSet<Message> Messages => Set<Message>();
}