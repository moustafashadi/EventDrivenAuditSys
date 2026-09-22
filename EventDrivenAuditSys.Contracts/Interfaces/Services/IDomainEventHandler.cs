using EventDrivenAuditSys.Core.Events;
using MediatR;

namespace EventDrivenAuditSys.Contracts.Interfaces.Services;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{ }
