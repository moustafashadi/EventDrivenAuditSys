using EventDrivenAuditSys.Core.Entities.Users;

namespace EventDrivenAuditSys.Core.IServices.Repositories.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);
}
