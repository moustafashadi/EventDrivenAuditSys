using MediatR;

namespace EventDrivenAuditSys.Contracts.Interfaces.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse> { }
