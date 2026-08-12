using TheZone.Application.Abstractions.Messaging;

namespace TheZone.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string PhoneNumber, string DisplayName) : ICommand<Guid>;