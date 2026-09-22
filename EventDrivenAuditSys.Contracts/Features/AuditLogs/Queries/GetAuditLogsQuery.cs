using EventDrivenAuditSys.Contracts.DTOs.Getter.Audit;
using EventDrivenAuditSys.Contracts.Interfaces.Cqrs;

namespace EventDrivenAuditSys.Contracts.Features.AuditLogs.Queries;

public sealed record GetAuditLogsQuery(Guid? UserId = null) : IQuery<IReadOnlyList<AuditLogDto>>;
