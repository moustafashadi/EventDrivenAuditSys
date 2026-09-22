using EventDrivenAuditSys.Contracts.DTOs.Getter.Enrollments;
using EventDrivenAuditSys.Contracts.Features.Enrollments.Commands;
using EventDrivenAuditSys.Contracts.Features.Enrollments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventDrivenAuditSys.API.Controllers;

[ApiController]
[Route("api/enrollments")]
[Produces("application/json")]
public sealed class EnrollmentsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(EnrollCourseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Enroll([FromBody] EnrollCourseCommand command, CancellationToken cancellationToken)
    {
        var enrollmentId = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetByUser),
            new { userId = command.UserId },
            new EnrollCourseResponse(enrollmentId));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EnrollmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByUser([FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        if (userId is null || userId == Guid.Empty)
        {
            return BadRequest("The 'userId' query parameter is required.");
        }

        return Ok(await mediator.Send(new GetEnrollmentsByUserQuery(userId.Value), cancellationToken));
    }
}
