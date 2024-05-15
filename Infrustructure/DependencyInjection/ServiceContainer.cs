using Application.Extensions.Identity;
using Application.Interface.Identity;
using Infrustructure.DataAccess;
using Infrustructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrustructure.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(o=>o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

        }).AddIdentityCookies();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddAuthorizationBuilder()
            .AddPolicy("AdministrationPolicy", adp =>
            {
                adp.RequireAuthenticatedUser();
                adp.RequireRole("Admin","Manager");
            })
            .AddPolicy("UserPolicy", adp =>
            {
                adp.RequireAuthenticatedUser();
                adp.RequireRole("User");
            });
        services.AddCascadingAuthenticationState();
        services.AddScoped<IAccount, Account>();
        return services;

    }
}
