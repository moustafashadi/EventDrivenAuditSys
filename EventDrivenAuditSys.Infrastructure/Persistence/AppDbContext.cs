using EventDrivenAuditSys.Core.Entities.Audit;
using EventDrivenAuditSys.Core.Entities.Courses;
using EventDrivenAuditSys.Core.Entities.Enrollments;
using EventDrivenAuditSys.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenAuditSys.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
