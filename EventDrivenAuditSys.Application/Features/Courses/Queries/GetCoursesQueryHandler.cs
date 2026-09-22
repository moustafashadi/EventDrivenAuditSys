using EventDrivenAuditSys.Contracts.DTOs.Getter.Courses;
using EventDrivenAuditSys.Contracts.Features.Courses.Queries;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;
using EventDrivenAuditSys.Core.IServices.Repositories.Courses;

namespace EventDrivenAuditSys.Application.Features.Courses.Queries;

public sealed class GetCoursesQueryHandler(ICourseRepository courses)
    : IQueryHandler<GetCoursesQuery, IReadOnlyList<CourseDto>>
{
    public async Task<IReadOnlyList<CourseDto>> Handle(GetCoursesQuery query, CancellationToken cancellationToken) =>
        (await courses.GetAllAsync(cancellationToken))
            .Select(course => new CourseDto(course.Id, course.Title, course.Description, course.Price))
            .ToList();
}
