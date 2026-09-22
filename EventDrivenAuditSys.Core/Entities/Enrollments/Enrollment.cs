using EventDrivenAuditSys.Core.Entities;
using EventDrivenAuditSys.Core.Entities.Courses;
using EventDrivenAuditSys.Core.Entities.Users;
using EventDrivenAuditSys.Core.Events.Enrollments;

namespace EventDrivenAuditSys.Core.Entities.Enrollments;

public sealed class Enrollment : BaseEntity
{
    public Guid UserId { get; private set; }

    public Guid CourseId { get; private set; }

    public DateTime EnrolledAtUtc { get; private set; }

    public User User { get; private set; } = null!;

    public Course Course { get; private set; } = null!;

    private Enrollment()
    {
    }

    public static Enrollment Create(User user, Course course, DateTime enrolledAtUtc)
    {
        var enrollment = new Enrollment
        {
            UserId = user.Id,
            CourseId = course.Id,
            EnrolledAtUtc = enrolledAtUtc
        };

        enrollment.RaiseDomainEvent(new EnrollmentCreatedDomainEvent(
            EnrollmentId: enrollment.Id,
            UserId: user.Id,
            CourseId: course.Id,
            CourseTitle: course.Title,
            OccurredAtUtc: enrolledAtUtc));

        return enrollment;
    }
}
