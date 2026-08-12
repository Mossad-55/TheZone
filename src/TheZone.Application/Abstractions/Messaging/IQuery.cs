using MediatR;
using TheZone.Application.Common.Results;

namespace TheZone.Application.Abstractions.Messaging;

public interface IQuery<TResponse> 
    : IRequest<Result<TResponse>>
{
    
}