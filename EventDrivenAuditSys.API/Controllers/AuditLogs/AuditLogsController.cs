using EventDrivenAuditSys.Contracts.DTOs.Getter.Audit;
using EventDrivenAuditSys.Contracts.Features.AuditLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventDrivenAuditSys.API.Controllers;

[ApiController]
[Route("api/audit-logs")]
[Produces("application/json")]
public sealed class AuditLogsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AuditLogDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuditLogs([FromQuery] Guid? userId, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetAuditLogsQuery(userId), cancellationToken));
}
