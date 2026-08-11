using MediatR;
using TheZone.Application.Common.Results;

namespace TheZone.Application.Abstractions.Messaging;
public interface ICommandHandler<TCommand> : IRequestHandler<ICommand, Result>
    where TCommand : ICommand
{
}
