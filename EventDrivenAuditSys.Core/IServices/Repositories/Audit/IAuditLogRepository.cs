using EventDrivenAuditSys.Core.Entities.Audit;

namespace EventDrivenAuditSys.Core.IServices.Repositories.Audit;

public interface IAuditLogRepository
{
    Task AddRangeAsync(IEnumerable<AuditLog> auditLogs, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AuditLog>> GetAsync(Guid? userId, int maxResults = 200, CancellationToken cancellationToken = default);
}
