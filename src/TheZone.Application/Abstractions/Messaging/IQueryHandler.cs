using MediatR;
using TheZone.Application.Common.Results;

namespace TheZone.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery  : IQuery<TResponse>
{
    
}