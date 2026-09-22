using EventDrivenAuditSys.Contracts.Interfaces.Commands;

namespace EventDrivenAuditSys.Contracts.Features.Enrollments.Commands;

public sealed record EnrollCourseCommand(Guid UserId, Guid CourseId) : ICommand<Guid>;
