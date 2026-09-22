namespace EventDrivenAuditSys.Contracts.Features.AuditLogs;

public sealed record AuditEvent(
    Guid UserId,
    string Action,
    string EntityName,
    Guid EntityId,
    string? Metadata,
    DateTime OccurredAtUtc);
