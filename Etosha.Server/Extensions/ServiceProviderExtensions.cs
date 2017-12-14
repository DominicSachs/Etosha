using Microsoft.Extensions.DependencyInjection;
using System;

namespace Etosha.Server.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            provider.GetService<AppDbContext>().Database.Migrate();
        }
    }
}
