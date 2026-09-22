using EventDrivenAuditSys.Contracts.Exceptions;
using EventDrivenAuditSys.Contracts.Features.Enrollments.Commands;
using EventDrivenAuditSys.Contracts.Interfaces.Commands;
using EventDrivenAuditSys.Core.Entities.Enrollments;
using EventDrivenAuditSys.Core.IServices.Custom;
using EventDrivenAuditSys.Core.IServices.Repositories.Courses;
using EventDrivenAuditSys.Core.IServices.Repositories.Enrollments;
using EventDrivenAuditSys.Core.IServices.Repositories.Users;
using MediatR;

namespace EventDrivenAuditSys.Application.Features.Enrollments.Commands;

public sealed class EnrollCourseCommandHandler(
    IUserRepository users,
    ICourseRepository courses,
    IEnrollmentRepository enrollments,
    IUnitOfWork unitOfWork,
    IMediator mediator) : ICommandHandler<EnrollCourseCommand, Guid>
{
    public async Task<Guid> Handle(EnrollCourseCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId == Guid.Empty || command.CourseId == Guid.Empty)
        {
            throw new ArgumentException("UserId and CourseId must be valid, non-empty GUIDs.");
        }

        var user = await users.GetByIdAsync(command.UserId, cancellationToken)
            ?? throw new NotFoundException($"User '{command.UserId}' was not found.");

        var course = await courses.GetByIdAsync(command.CourseId, cancellationToken)
            ?? throw new NotFoundException($"Course '{command.CourseId}' was not found.");

        if (await enrollments.ExistsAsync(user.Id, course.Id, cancellationToken))
        {
            throw new ConflictException($"User '{user.Id}' is already enrolled in course '{course.Id}'.");
        }

        var enrollment = Enrollment.Create(user, course, DateTime.UtcNow);

        await enrollments.AddAsync(enrollment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in enrollment.DomainEvents)
            await mediator.Publish(domainEvent, cancellationToken);
        enrollment.ClearDomainEvents();

        return enrollment.Id;
    }
}
