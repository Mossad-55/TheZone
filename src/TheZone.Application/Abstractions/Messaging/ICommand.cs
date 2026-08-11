using MediatR;
using TheZone.Application.Common.Results;

namespace TheZone.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
    
}