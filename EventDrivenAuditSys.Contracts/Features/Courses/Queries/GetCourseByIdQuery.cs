using EventDrivenAuditSys.Contracts.DTOs.Getter.Courses;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;

namespace EventDrivenAuditSys.Contracts.Features.Courses.Queries;

public sealed record GetCourseByIdQuery(Guid Id) : IQuery<CourseDto>;
