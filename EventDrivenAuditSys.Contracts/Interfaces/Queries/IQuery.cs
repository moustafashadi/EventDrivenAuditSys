using MediatR;

namespace EventDrivenAuditSys.Contracts.Interfaces.Cqrs;

public interface IQuery<TResponse> : IRequest<TResponse> { }
