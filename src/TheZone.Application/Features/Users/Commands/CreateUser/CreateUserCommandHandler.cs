using Microsoft.EntityFrameworkCore;
using TheZone.Application.Abstractions.Data;
using TheZone.Application.Abstractions.Messaging;
using TheZone.Application.Common.Results;
using TheZone.Application.Features.Users.Errors;
using TheZone.Domain.Users;

namespace TheZone.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler 
    : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var phoneNumberExists = await _context.Users.AnyAsync(
            user => user.PhoneNumber == request.PhoneNumber,
            cancellationToken
        );

        if (phoneNumberExists)
        {
            return Result<Guid>.Failure(UserErrors.PhoneNumberAlreadyExists);
        }

        var user = User.Create(
            request.PhoneNumber,
            request.DisplayName
        );

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(user.Id);
    }
}