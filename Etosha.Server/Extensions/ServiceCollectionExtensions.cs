using Etosha.Server.Common.Execution;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using Etosha.Server.Execution;
using Etosha.Server.Providers.Implementations;
using Etosha.Server.Providers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Etosha.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            // services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        }

        public static void AddIdentityFramework(this IServiceCollection services, PasswordOptions options)
        {
            services.AddIdentity<AppUser, AppRole>(setup =>
            {
                setup.Password = options;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void AddEtoshaServices(this IServiceCollection services)
        {
            services.AddScoped<IActionExecutor, ActionExecutor>();
            services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
        }
    }
}
