using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sandbox.App.Interfaces.Auth;
using Sandbox.App.Interfaces.Common;
using Sandbox.App.Interfaces.Repositories;
using Sandbox.DataAccess.Persistence;
using Sandbox.DataAccess.Repositories;
using Sandbox.DataAccess.Services;

namespace Sandbox.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SandboxConnection");

        var envConn = Environment.GetEnvironmentVariable("ConnectionStrings__SandboxConnection");
        if (!string.IsNullOrEmpty(envConn))
        {
            connectionString = envConn;
        }

        if (string.IsNullOrEmpty(connectionString) || configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
        {
            connectionString = "Host=host.docker.internal;Port=5432;Database=Sandbox_v1;Username=postgres;Password=admin";
        }

        // Регистрируем SandboxDbContext с пулом
        services.AddDbContextPool<SandboxDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "sandbox");
                    })
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging();
        }, poolSize: 128); // Размер пула по умолчанию 128, можно изменить

        services.AddScoped<ISandboxDbContext>(provider => provider.GetRequiredService<ISandboxDbContext>());
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
