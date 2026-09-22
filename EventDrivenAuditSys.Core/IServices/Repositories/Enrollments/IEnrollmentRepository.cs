using EventDrivenAuditSys.Core.Entities.Enrollments;

namespace EventDrivenAuditSys.Core.IServices.Repositories.Enrollments;

public interface IEnrollmentRepository
{
    Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Enrollment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
