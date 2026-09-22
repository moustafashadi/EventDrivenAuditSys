using EventDrivenAuditSys.Core.IServices.Custom;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenAuditSys.Infrastructure.Persistence;

public sealed class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}
