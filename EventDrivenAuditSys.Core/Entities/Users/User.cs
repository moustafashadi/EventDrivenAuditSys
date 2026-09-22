using EventDrivenAuditSys.Core.Entities;

namespace EventDrivenAuditSys.Core.Entities.Users;

public sealed class User : BaseEntity
{
    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    private User()
    {
    }

    public User(Guid id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }
}
