using MediatR;

namespace EventDrivenAuditSys.Contracts.Interfaces.Queries;

public interface IQuery<TResponse> : IRequest<TResponse> { }
