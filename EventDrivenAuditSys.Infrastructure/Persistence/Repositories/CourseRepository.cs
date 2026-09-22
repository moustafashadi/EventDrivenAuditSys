using EventDrivenAuditSys.Core.Entities.Courses;
using EventDrivenAuditSys.Core.IServices.Repositories.Courses;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenAuditSys.Infrastructure.Persistence.Repositories;

public sealed class CourseRepository(AppDbContext context) : ICourseRepository
{
    public Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Courses.AsNoTracking().FirstOrDefaultAsync(course => course.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Course>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Courses.AsNoTracking().OrderBy(course => course.Title).ToListAsync(cancellationToken);
}
