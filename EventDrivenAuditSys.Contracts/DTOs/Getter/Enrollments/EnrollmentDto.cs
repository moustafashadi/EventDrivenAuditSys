namespace EventDrivenAuditSys.Contracts.DTOs.Getter.Enrollments;

public sealed record EnrollmentDto(
    Guid Id,
    Guid UserId,
    Guid CourseId,
    string CourseTitle,
    decimal CoursePrice,
    DateTime EnrolledAtUtc);
