using MediatR;

namespace EventDrivenAuditSys.Contracts.Interfaces.Cqrs;

public interface ICommand : IRequest { }

public interface ICommand<TResponse> : IRequest<TResponse> { }
