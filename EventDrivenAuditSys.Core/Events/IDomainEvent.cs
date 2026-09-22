using MediatR;

namespace EventDrivenAuditSys.Core.Events;

public interface IDomainEvent : INotification
{
}
