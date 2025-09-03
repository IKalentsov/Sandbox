using Sandbox.DataAccess.Persistence;

namespace Sandbox.Helpers;

internal class PostgreHelper
{
    public static async Task WaitForPostgres(IServiceProvider services, ILogger logger, CancellationToken cancellationToken = default)
    {
        var maxAttempts = 30;
        var delay = TimeSpan.FromSeconds(2);

        for (int i = 1; i <= maxAttempts; i++)
        {
            try
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<SandboxDbContext>();

                logger.LogInformation("Checking database connection (attempt {Attempt}/{MaxAttempts})", i, maxAttempts);

                // Простая проверка подключения
                var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

                if (canConnect)
                {
                    logger.LogInformation("Database is ready");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Database not ready yet (attempt {Attempt}/{MaxAttempts})", i, maxAttempts);

                if (i == maxAttempts)
                {
                    logger.LogError(ex, "Database connection failed after {MaxAttempts} attempts", maxAttempts);
                    throw;
                }

                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}
