namespace EventDrivenAuditSys.Contracts.Exceptions;

public sealed class ConflictException(string message) : Exception(message);
