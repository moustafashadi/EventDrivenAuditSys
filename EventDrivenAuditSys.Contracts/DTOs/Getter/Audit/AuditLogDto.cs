namespace EventDrivenAuditSys.Contracts.DTOs.Getter.Audit;

public sealed record AuditLogDto(
    Guid Id,
    Guid UserId,
    string Action,
    string EntityName,
    Guid EntityId,
    DateTime CreatedAtUtc,
    string? Metadata);
