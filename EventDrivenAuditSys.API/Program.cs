using EventDrivenAuditSys.API.Middlewares;
using EventDrivenAuditSys.Application;
using EventDrivenAuditSys.Infrastructure;
using EventDrivenAuditSys.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Event-Driven Audit API",
        Version = "v1",
        Description = "Event-driven audit system built with Clean Architecture, CQRS, and MediatR."
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

await DatabaseInitializer.InitializeAsync(app.Services);

app.Run();
