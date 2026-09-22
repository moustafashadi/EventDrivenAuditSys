using EventDrivenAuditSys.Core.Entities.Enrollments;
using EventDrivenAuditSys.Core.IServices.Repositories.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenAuditSys.Infrastructure.Persistence.Repositories;

public sealed class EnrollmentRepository(AppDbContext context) : IEnrollmentRepository
{
    public async Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default) =>
        await context.Enrollments.AddAsync(enrollment, cancellationToken);

    public Task<bool> ExistsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken = default) =>
        context.Enrollments.AnyAsync(
            enrollment => enrollment.UserId == userId && enrollment.CourseId == courseId,
            cancellationToken);

    public async Task<IReadOnlyList<Enrollment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await context.Enrollments
            .AsNoTracking()
            .Include(enrollment => enrollment.Course)
            .Where(enrollment => enrollment.UserId == userId)
            .OrderByDescending(enrollment => enrollment.EnrolledAtUtc)
            .ToListAsync(cancellationToken);
}
