using FastEndpoints;
using FastEndpoints.Swagger;
using Kata08ConflictingObjectives.Application;
using Kata08ConflictingObjectives.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationConfigureServices(builder.Configuration);
builder.Services.AddInfrastructureConfigureServices(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

var app = builder.Build();

app.UseFastEndpoints();
app.RunInfrastructurePersistenceMigration(app.Environment.IsDevelopment());

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.Run();

