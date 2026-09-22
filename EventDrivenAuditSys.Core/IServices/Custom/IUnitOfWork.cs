namespace EventDrivenAuditSys.Core.IServices.Custom;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
