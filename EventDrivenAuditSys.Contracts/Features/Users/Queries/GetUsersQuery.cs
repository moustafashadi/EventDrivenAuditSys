using EventDrivenAuditSys.Contracts.DTOs.Getter.Users;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;

namespace EventDrivenAuditSys.Contracts.Features.Users.Queries;

public sealed record GetUsersQuery() : IQuery<IReadOnlyList<UserDto>>;
