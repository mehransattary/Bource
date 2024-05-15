using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class SerivceContainer
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}
