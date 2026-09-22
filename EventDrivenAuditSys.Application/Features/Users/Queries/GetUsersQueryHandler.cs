using EventDrivenAuditSys.Contracts.DTOs.Getter.Users;
using EventDrivenAuditSys.Contracts.Features.Users.Queries;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;
using EventDrivenAuditSys.Core.IServices.Repositories.Users;

namespace EventDrivenAuditSys.Application.Features.Users.Queries;

public sealed class GetUsersQueryHandler(IUserRepository users)
    : IQueryHandler<GetUsersQuery, IReadOnlyList<UserDto>>
{
    public async Task<IReadOnlyList<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken) =>
        (await users.GetAllAsync(cancellationToken))
            .Select(user => new UserDto(user.Id, user.FullName, user.Email))
            .ToList();
}
