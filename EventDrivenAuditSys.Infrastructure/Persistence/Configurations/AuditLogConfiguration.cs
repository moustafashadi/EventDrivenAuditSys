using EventDrivenAuditSys.Core.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventDrivenAuditSys.Infrastructure.Persistence.Configurations;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(auditLog => auditLog.Id);

        builder.Property(auditLog => auditLog.Action).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.EntityName).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.Metadata);

        builder.HasIndex(auditLog => auditLog.UserId);
        builder.HasIndex(auditLog => auditLog.CreatedAtUtc);
    }
}
