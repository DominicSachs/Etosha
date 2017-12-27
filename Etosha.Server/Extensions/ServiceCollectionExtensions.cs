using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Etosha.Server.EntityFramework;
using Etosha.Server.Execution;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Etosha.Server.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddEntityFramework(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
		}

		public static void AddIdentityFramework(this IServiceCollection services, PasswordOptions options)
		{
			services.AddIdentityCore<AppUser>(setup =>
			{
				setup.Password = options;
			})
			.AddEntityFrameworkStores<AppDbContext>()
			.AddRoles<AppRole>()
			// It seems there is no AddDefaultTokenProviders in NetStandard 2.0 at the moment
			//.AddTokenProvider<DataProtectorTokenProvider>(TokenOptions.DefaultProvider)
			.AddTokenProvider<EmailTokenProvider<AppUser>>(TokenOptions.DefaultEmailProvider)
			.AddTokenProvider<PhoneNumberTokenProvider<AppUser>>(TokenOptions.DefaultPhoneProvider)
			.AddTokenProvider<AuthenticatorTokenProvider<AppUser>>(TokenOptions.DefaultAuthenticatorProvider);
		}

		public static void AddActionExecutor(this IServiceCollection services)
		{
			services.AddScoped<IActionExecutor, ActionExecutor>();
		}
	}
}
