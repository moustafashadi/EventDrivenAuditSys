namespace EventDrivenAuditSys.Core.Events.Enrollments;

public sealed record EnrollmentCreatedDomainEvent(
    Guid EnrollmentId,
    Guid UserId,
    Guid CourseId,
    string CourseTitle,
    DateTime OccurredAtUtc) : IDomainEvent;
