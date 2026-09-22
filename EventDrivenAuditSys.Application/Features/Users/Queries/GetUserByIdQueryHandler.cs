using EventDrivenAuditSys.Contracts.DTOs.Getter.Users;
using EventDrivenAuditSys.Contracts.Exceptions;
using EventDrivenAuditSys.Contracts.Features.Users.Queries;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;
using EventDrivenAuditSys.Core.IServices.Repositories.Users;

namespace EventDrivenAuditSys.Application.Features.Users.Queries;

public sealed class GetUserByIdQueryHandler(IUserRepository users)
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await users.GetByIdAsync(query.Id, cancellationToken)
            ?? throw new NotFoundException($"User '{query.Id}' was not found.");

        return new UserDto(user.Id, user.FullName, user.Email);
    }
}
