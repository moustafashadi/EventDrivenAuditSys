using EventDrivenAuditSys.Contracts.DTOs.Getter.Courses;
using EventDrivenAuditSys.Contracts.Features.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventDrivenAuditSys.API.Controllers;

[ApiController]
[Route("api/courses")]
[Produces("application/json")]
public sealed class CoursesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CourseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourses(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetCoursesQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourse(Guid id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetCourseByIdQuery(id), cancellationToken));
}
