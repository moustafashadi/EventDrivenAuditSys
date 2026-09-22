using EventDrivenAuditSys.Core.Entities;

namespace EventDrivenAuditSys.Core.Entities.Courses;

public sealed class Course : BaseEntity
{
    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    private Course()
    {
    }

    public Course(Guid id, string title, string? description, decimal price)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
    }
}
