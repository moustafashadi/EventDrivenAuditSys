using EventDrivenAuditSys.Core.Entities;

namespace EventDrivenAuditSys.Core.Entities.Audit;

public sealed class AuditLog : BaseEntity
{
    public Guid UserId { get; private set; }

    public string Action { get; private set; } = string.Empty;

    public string EntityName { get; private set; } = string.Empty;

    public Guid EntityId { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public string? Metadata { get; private set; }

    private AuditLog()
    {
    }

    public AuditLog(Guid userId, string action, string entityName, Guid entityId, DateTime createdAtUtc, string? metadata)
    {
        UserId = userId;
        Action = action;
        EntityName = entityName;
        EntityId = entityId;
        CreatedAtUtc = createdAtUtc;
        Metadata = metadata;
    }
}
