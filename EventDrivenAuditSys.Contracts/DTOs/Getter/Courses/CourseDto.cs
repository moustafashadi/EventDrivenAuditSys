namespace EventDrivenAuditSys.Contracts.DTOs.Getter.Courses;

public sealed record CourseDto(Guid Id, string Title, string? Description, decimal Price);
