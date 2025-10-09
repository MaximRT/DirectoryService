using Application.DependencyInjection;
using Infrastructure.Postgres.DependencyInjection;
using Microsoft.OpenApi.Models;
using Presentation.Extentions;
using Serilog;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, _) =>
    {
        if (context.JsonTypeInfo.Type == typeof(Envelope<Errors>))
        {
            if (schema.Properties.TryGetValue("errors", out var errorsProps))
            {
                errorsProps.Items.Reference = new OpenApiReference()
                {
                    Type = ReferenceType.Schema,
                    Id = "Error",
                };
            }
        }

        return Task.CompletedTask;
    });
});

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext();
});

builder.Services.AddInfrastructurePostgres(builder.Configuration);
builder.Services.AddApplicationDependencies();

var app = builder.Build();

app.UseExceptionMiddleware();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DirectoryService"));
}

app.MapControllers();

app.Run();