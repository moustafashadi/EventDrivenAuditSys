using EventDrivenAuditSys.Core.Entities.Audit;
using EventDrivenAuditSys.Core.IServices.Repositories.Audit;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenAuditSys.Infrastructure.Persistence.Repositories;

public sealed class AuditLogRepository(AppDbContext context) : IAuditLogRepository
{
    public async Task AddRangeAsync(IEnumerable<AuditLog> auditLogs, CancellationToken cancellationToken = default) =>
        await context.AuditLogs.AddRangeAsync(auditLogs, cancellationToken);

    public async Task<IReadOnlyList<AuditLog>> GetAsync(
        Guid? userId,
        int maxResults = 200,
        CancellationToken cancellationToken = default) =>
        await context.AuditLogs
            .AsNoTracking()
            .Where(auditLog => userId == null || auditLog.UserId == userId)
            .OrderByDescending(auditLog => auditLog.CreatedAtUtc)
            .Take(maxResults)
            .ToListAsync(cancellationToken);
}
