using MediatR;
using TheZone.Application.Common.Results;

namespace TheZone.Application.Abstractions.Messaging;
public interface ICommandHandler<TCommand> 
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
    
}
