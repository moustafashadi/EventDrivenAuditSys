using EventDrivenAuditSys.Contracts.DTOs.Getter.Courses;
using EventDrivenAuditSys.Contracts.Exceptions;
using EventDrivenAuditSys.Contracts.Features.Courses.Queries;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;
using EventDrivenAuditSys.Core.IServices.Repositories.Courses;

namespace EventDrivenAuditSys.Application.Features.Courses.Queries;

public sealed class GetCourseByIdQueryHandler(ICourseRepository courses)
    : IQueryHandler<GetCourseByIdQuery, CourseDto>
{
    public async Task<CourseDto> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
    {
        var course = await courses.GetByIdAsync(query.Id, cancellationToken)
            ?? throw new NotFoundException($"Course '{query.Id}' was not found.");

        return new CourseDto(course.Id, course.Title, course.Description, course.Price);
    }
}
