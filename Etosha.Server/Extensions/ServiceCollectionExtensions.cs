using Etosha.Server.Common.Execution;
using Etosha.Server.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace Etosha.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        //{
        //    services.AddEntityFramework().AddSqlServer().AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        //}

        //public static void AddScopedAppContext(this IServiceCollection services)
        //{
        //    services.AddScoped<IDbContext, AppDbContext>();
        //}

        //public static void AddIdentityFramework(this IServiceCollection services, PasswordOptions options)
        //{
        //    services.AddIdentity<AppUser, AppRole>(setup =>
        //    {
        //        setup.Password = options;
        //    })
        //    .AddEntityFrameworkStores<AppDbContext, Guid>()
        //    .AddDefaultTokenProviders();
        //}

        public static void AddActionExecutor(this IServiceCollection services)
        {
            services.AddScoped<IActionExecutor, ActionExecutor>();
        }
    }
}
