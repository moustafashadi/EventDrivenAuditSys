using EventDrivenAuditSys.Contracts.DTOs.Getter.Enrollments;
using EventDrivenAuditSys.Contracts.Features.Enrollments.Queries;
using EventDrivenAuditSys.Contracts.Interfaces.Queries;
using EventDrivenAuditSys.Core.IServices.Repositories.Enrollments;

namespace EventDrivenAuditSys.Application.Features.Enrollments.Queries;

public sealed class GetEnrollmentsByUserQueryHandler(IEnrollmentRepository enrollments)
    : IQueryHandler<GetEnrollmentsByUserQuery, IReadOnlyList<EnrollmentDto>>
{
    public async Task<IReadOnlyList<EnrollmentDto>> Handle(GetEnrollmentsByUserQuery query, CancellationToken cancellationToken) =>
        (await enrollments.GetByUserIdAsync(query.UserId, cancellationToken))
            .Select(enrollment => new EnrollmentDto(
                enrollment.Id,
                enrollment.UserId,
                enrollment.CourseId,
                enrollment.Course.Title,
                enrollment.Course.Price,
                enrollment.EnrolledAtUtc))
            .ToList();
}
