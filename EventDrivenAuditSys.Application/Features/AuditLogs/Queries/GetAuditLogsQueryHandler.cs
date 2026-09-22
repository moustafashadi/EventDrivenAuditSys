using EventDrivenAuditSys.Contracts.DTOs.Getter.Audit;
using EventDrivenAuditSys.Contracts.Features.AuditLogs;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;
using EventDrivenAuditSys.Core.IServices.Repositories.Audit;

namespace EventDrivenAuditSys.Application.Features.AuditLogs.Queries;

public sealed class GetAuditLogsQueryHandler(IAuditLogRepository auditLogs)
    : IQueryHandler<GetAuditLogsQuery, IReadOnlyList<AuditLogDto>>
{
    public async Task<IReadOnlyList<AuditLogDto>> Handle(GetAuditLogsQuery query, CancellationToken cancellationToken) =>
        (await auditLogs.GetAsync(query.UserId, cancellationToken: cancellationToken))
            .Select(log => new AuditLogDto(
                log.Id,
                log.UserId,
                log.Action,
                log.EntityName,
                log.EntityId,
                log.CreatedAtUtc,
                log.Metadata))
            .ToList();
}
