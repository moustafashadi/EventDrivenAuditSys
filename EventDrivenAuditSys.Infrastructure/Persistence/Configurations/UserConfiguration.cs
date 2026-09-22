using EventDrivenAuditSys.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventDrivenAuditSys.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.FullName).HasMaxLength(100).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(200).IsRequired();

        builder.HasData(
            new User(SeedData.Users.Alice, "Alice Smith", "alice@example.com"),
            new User(SeedData.Users.Bob, "Bob Jones", "bob@example.com"));
    }
}
