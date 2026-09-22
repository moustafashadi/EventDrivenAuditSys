using MediatR;

namespace EventDrivenAuditSys.Contracts.Interfaces.Commands;

public interface ICommand : IRequest { }

public interface ICommand<TResponse> : IRequest<TResponse> { }
