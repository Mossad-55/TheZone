using Microsoft.EntityFrameworkCore;
using TheZone.Domain.Chats;
using TheZone.Domain.Users;

namespace TheZone.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Chat> Chats { get; }
    DbSet<ChatParticipant> ChatParticipants { get; }
    DbSet<Message> Messages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}