using EventDrivenAuditSys.Core.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventDrivenAuditSys.Infrastructure.Persistence.Configurations;

internal sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(course => course.Id);

        builder.Property(course => course.Title).HasMaxLength(150).IsRequired();
        builder.Property(course => course.Description).HasMaxLength(1000);

        builder.HasData(
            new Course(
                SeedData.Courses.CSharpFundamentals,
                "C# Fundamentals",
                "Syntax, types, collections and the basics of object-oriented programming.",
                49.00m),
            new Course(
                SeedData.Courses.CleanArchitecture,
                "Clean Architecture in .NET",
                "Domain, Application, Infrastructure and API layers; CQRS and domain events.",
                89.00m),
            new Course(
                SeedData.Courses.AspNetCoreWebApi,
                "ASP.NET Core Web API Masterclass",
                "Building production-grade HTTP APIs with ASP.NET Core.",
                199.00m));
    }
}
