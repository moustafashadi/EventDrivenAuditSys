using System.Threading.Channels;
using EventDrivenAuditSys.Contracts.Features.AuditLogs;
using EventDrivenAuditSys.Contracts.Interfaces.Services.Audit;
using Microsoft.Extensions.Logging;

namespace EventDrivenAuditSys.Application.Services.Audit;

public sealed class AuditEventQueue(ILogger<AuditEventQueue> logger) : IAuditEventQueue
{
    private const int Capacity = 1000;

    private readonly Channel<AuditEvent> _channel = Channel.CreateBounded<AuditEvent>(
        new BoundedChannelOptions(Capacity)
        {
            SingleReader = true,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.DropWrite
        });

    public ValueTask EnqueueAsync(AuditEvent auditEvent, CancellationToken cancellationToken = default)
    {
        if (!_channel.Writer.TryWrite(auditEvent))
        {
            logger.LogWarning(
                "Audit queue is full (capacity {Capacity}); audit event '{Action}' for {EntityName} '{EntityId}' was dropped.",
                Capacity, auditEvent.Action, auditEvent.EntityName, auditEvent.EntityId);
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask<AuditEvent> DequeueAsync(CancellationToken cancellationToken = default) =>
        _channel.Reader.ReadAsync(cancellationToken);

    public bool TryDequeue(out AuditEvent auditEvent) =>
        _channel.Reader.TryRead(out auditEvent!);
}
