using Etosha.Server.Common;
using Etosha.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Etosha.Server.EntityFramework
{
	internal class DbInitializer
	{
		internal static async Task Seed(IServiceProvider provider)
		{
			var context = provider.GetRequiredService<AppDbContext>();
			var roleManager = provider.GetRequiredService<RoleManager<AppRole>>();
			var userManager = provider.GetRequiredService<UserManager<AppUser>>();

			if (!context.Users.Any())
			{
				await roleManager.CreateAsync(new AppRole(Constants.AdministratorRoleName));
				await roleManager.CreateAsync(new AppRole(Constants.UserRoleName));

				var user = new AppUser("admin", "The", "Administrator", "admin@admin.com");
				await userManager.CreateAsync(user, "a");
				await userManager.AddToRoleAsync(user, Constants.AdministratorRoleName);

				user = new AppUser("sam", "Sam", "Sample", "sam@sample.com");
				await userManager.CreateAsync(user, "a");
				await userManager.AddToRoleAsync(user, Constants.UserRoleName);
			}
		}
	}
}
