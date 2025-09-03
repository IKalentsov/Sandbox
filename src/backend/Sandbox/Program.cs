using Microsoft.EntityFrameworkCore;
using Sandbox.App;
using Sandbox.DataAccess;
using Sandbox.DataAccess.Persistence;
using Sandbox.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// регистраци¤ сервисов приложени¤, относ¤щиес¤ к бизнес-логике
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Применяем миграции при запуске
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var configuration = services.GetRequiredService<IConfiguration>();

    try
    {
        // Ждем, пока база данных будет готова
        await PostgreHelper.WaitForPostgres(services, logger);

        logger.LogInformation("Applying database migrations...");

        var dbContext = services.GetRequiredService<SandboxDbContext>();

        // Проверяем, нужны ли миграции
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Applying {Count} pending migrations: {Migrations}",
                pendingMigrations.Count(), string.Join(", ", pendingMigrations));

            // Применяем миграции
            await dbContext.Database.MigrateAsync();

            logger.LogInformation("Database migrations applied successfully");
        }
        else
        {
            logger.LogInformation("No pending migrations found");
        }
    }
    catch (Exception ex)
    {
        logger.LogCritical(ex, "Failed to apply database migrations");

        // В зависимости от требований, либо прерываем запуск, либо продолжаем
        if (configuration.GetValue<bool>("Database:RequireMigrations", true))
        {
            throw new Exception("Application startup failed due to database migration issues", ex);
        }

        logger.LogWarning("Application will continue without applying database migrations");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
