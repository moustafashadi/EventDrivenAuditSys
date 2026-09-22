namespace EventDrivenAuditSys.Application.Configuration;

public sealed class AuditOptions
{
    public const string SectionName = "Audit";

    public int SimulatedWriteDelayMilliseconds { get; set; }

    public int MaxBatchSize { get; set; } = 32;
}
