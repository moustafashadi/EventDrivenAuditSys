using EventDrivenAuditSys.Application.Configuration;
using EventDrivenAuditSys.Application.Services.Audit;
using EventDrivenAuditSys.Contracts.Interfaces.Services.Audit;
using EventDrivenAuditSys.Core.IServices.Custom;
using EventDrivenAuditSys.Core.IServices.Repositories.Audit;
using EventDrivenAuditSys.Core.IServices.Repositories.Courses;
using EventDrivenAuditSys.Core.IServices.Repositories.Enrollments;
using EventDrivenAuditSys.Core.IServices.Repositories.Users;
using EventDrivenAuditSys.Infrastructure.Persistence;
using EventDrivenAuditSys.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventDrivenAuditSys.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Database")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        services.AddSingleton<IAuditEventQueue, AuditEventQueue>();
        services.AddHostedService<AuditLogBackgroundService>();

        services.Configure<AuditOptions>(configuration.GetSection(AuditOptions.SectionName));

        return services;
    }
}
