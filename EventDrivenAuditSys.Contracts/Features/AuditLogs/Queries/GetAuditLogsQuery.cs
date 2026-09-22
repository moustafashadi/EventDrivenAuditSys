using EventDrivenAuditSys.Contracts.DTOs.Getter.Audit;
using EventDrivenAuditSys.Contracts.Interfaces.Services;

namespace EventDrivenAuditSys.Contracts.Interfaces.Queries;

public sealed record GetAuditLogsQuery(Guid? UserId = null) : IQuery<IReadOnlyList<AuditLogDto>>;
