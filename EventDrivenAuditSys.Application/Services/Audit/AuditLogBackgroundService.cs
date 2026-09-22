using EventDrivenAuditSys.Application.Configuration;
using EventDrivenAuditSys.Contracts.Features.AuditLogs;
using EventDrivenAuditSys.Contracts.Interfaces.Services.Audit;
using EventDrivenAuditSys.Core.Entities.Audit;
using EventDrivenAuditSys.Core.IServices.Repositories.Audit;
using EventDrivenAuditSys.Core.IServices.Custom;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventDrivenAuditSys.Application.Services.Audit;

public sealed class AuditLogBackgroundService(
    IServiceScopeFactory scopeFactory,
    IAuditEventQueue queue,
    IOptions<AuditOptions> options,
    ILogger<AuditLogBackgroundService> logger) : BackgroundService
{
    private readonly AuditOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Audit log background worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var firstEvent = await queue.DequeueAsync(stoppingToken);

                var batch = new List<AuditEvent>(_options.MaxBatchSize) { firstEvent };
                while (batch.Count < _options.MaxBatchSize && queue.TryDequeue(out var nextEvent))
                {
                    batch.Add(nextEvent);
                }

                await ProcessBatchAsync(batch, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Processing an audit event batch failed; the batch will be skipped.");
            }
        }

        await DrainQueueOnShutdownAsync();

        logger.LogInformation("Audit log background worker stopped.");
    }

    private async Task ProcessBatchAsync(IReadOnlyList<AuditEvent> batch, CancellationToken cancellationToken)
    {
        if (_options.SimulatedWriteDelayMilliseconds > 0)
        {
            await Task.Delay(_options.SimulatedWriteDelayMilliseconds, cancellationToken);
        }

        using var scope = scopeFactory.CreateScope();
        var auditLogs = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await auditLogs.AddRangeAsync(batch.Select(ToAuditLog), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Persisted {Count} audit record(s).", batch.Count);
    }

    private async Task DrainQueueOnShutdownAsync()
    {
        try
        {
            var pending = new List<AuditEvent>();
            while (queue.TryDequeue(out var auditEvent))
            {
                pending.Add(auditEvent);
            }

            if (pending.Count > 0)
            {
                logger.LogInformation("Draining {Count} queued audit event(s) before shutdown.", pending.Count);
                await ProcessBatchAsync(pending, CancellationToken.None);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to drain the audit queue during shutdown.");
        }
    }

    private static AuditLog ToAuditLog(AuditEvent auditEvent) =>
        new(
            auditEvent.UserId,
            auditEvent.Action,
            auditEvent.EntityName,
            auditEvent.EntityId,
            auditEvent.OccurredAtUtc,
            auditEvent.Metadata);
}
