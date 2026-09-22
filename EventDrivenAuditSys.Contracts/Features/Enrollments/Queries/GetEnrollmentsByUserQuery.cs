using EventDrivenAuditSys.Contracts.DTOs.Getter.Enrollments;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;

namespace EventDrivenAuditSys.Contracts.Features.Enrollments.Queries;

public sealed record GetEnrollmentsByUserQuery(Guid UserId) : IQuery<IReadOnlyList<EnrollmentDto>>;
