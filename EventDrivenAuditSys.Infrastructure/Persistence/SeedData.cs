namespace EventDrivenAuditSys.Infrastructure.Persistence;

internal static class SeedData
{
    public static class Users
    {
        public static readonly Guid Alice = new("b3a1c2d3-4e5f-4a6b-8c7d-9e0f1a2b3c4d");
        public static readonly Guid Bob = new("c4b2d3e4-5f6a-4b7c-9d8e-0f1a2b3c4d5e");
    }

    public static class Courses
    {
        public static readonly Guid CSharpFundamentals = new("e6d4f5a6-7b8c-4d9e-1f0a-2b3c4d5e6f7a");
        public static readonly Guid CleanArchitecture = new("f7e5a6b7-8c9d-4e0f-2a1b-3c4d5e6f7a8b");
        public static readonly Guid AspNetCoreWebApi = new("a8f6b7c8-9d0e-4f1a-3b2c-4d5e6f7a8b9c");
    }
}
