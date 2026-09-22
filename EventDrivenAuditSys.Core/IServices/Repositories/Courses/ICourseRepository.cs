using EventDrivenAuditSys.Core.Entities.Courses;

namespace EventDrivenAuditSys.Core.IServices.Repositories.Courses;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Course>> GetAllAsync(CancellationToken cancellationToken = default);
}
