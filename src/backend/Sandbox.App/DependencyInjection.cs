using Microsoft.Extensions.DependencyInjection;
using Sandbox.App.Interfaces.Services;
using Sandbox.App.Services;

namespace Sandbox.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
