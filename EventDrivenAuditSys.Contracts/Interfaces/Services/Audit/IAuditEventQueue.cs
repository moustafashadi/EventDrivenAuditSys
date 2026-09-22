using EventDrivenAuditSys.Contracts.Features.AuditLogs;

namespace EventDrivenAuditSys.Contracts.Interfaces.Services.Audit;

public interface IAuditEventQueue
{
    ValueTask EnqueueAsync(AuditEvent auditEvent, CancellationToken cancellationToken = default);

    ValueTask<AuditEvent> DequeueAsync(CancellationToken cancellationToken = default);

    bool TryDequeue(out AuditEvent auditEvent);
}
