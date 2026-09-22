using System.Text.Json;
using EventDrivenAuditSys.Application.Configuration;
using EventDrivenAuditSys.Contracts.Features.AuditLogs;
using EventDrivenAuditSys.Contracts.Interfaces.Services;
using EventDrivenAuditSys.Contracts.Interfaces.Services.Audit;
using EventDrivenAuditSys.Core.Entities.Enrollments;
using EventDrivenAuditSys.Core.Events.Enrollments;

namespace EventDrivenAuditSys.Application.Services.Audit;

public sealed class EnrollmentCreatedAuditEventHandler(IAuditEventQueue auditEventQueue)
    : IDomainEventHandler<EnrollmentCreatedDomainEvent>
{
    public async Task Handle(EnrollmentCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var metadata = JsonSerializer.Serialize(new
        {
            courseId = domainEvent.CourseId,
            courseTitle = domainEvent.CourseTitle,
            enrolledAtUtc = domainEvent.OccurredAtUtc
        });

        var auditEvent = new AuditEvent(
            UserId: domainEvent.UserId,
            Action: AuditActions.EnrollCourse,
            EntityName: nameof(Enrollment),
            EntityId: domainEvent.EnrollmentId,
            Metadata: metadata,
            OccurredAtUtc: domainEvent.OccurredAtUtc);

        await auditEventQueue.EnqueueAsync(auditEvent, cancellationToken);
    }
}
