using Etosha.Server.ActionHandlers.UserActionHandlers;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Server.Tests.ActionHandlers.UserActionHandlers
{
	public sealed class DeleteUserActionHandlerTests
	{
		private UserManager<AppUser> _userManager;

		public DeleteUserActionHandlerTests()
		{
			var userStore = Substitute.For<IUserStore<AppUser>>();
			_userManager = Substitute.For<UserManager<AppUser>>(
				userStore,
				Substitute.For<IOptions<IdentityOptions>>(),
				Substitute.For<IPasswordHasher<AppUser>>(),
				Enumerable.Empty<IUserValidator<AppUser>>(),
				Enumerable.Empty<IPasswordValidator<AppUser>>(),
				Substitute.For<ILookupNormalizer>(),
				new IdentityErrorDescriber(),
				Substitute.For<IServiceProvider>(),
				Substitute.For<ILogger<UserManager<AppUser>>>());
		}

		[Fact]
		public async Task Should_Delete_A_User()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DeleteUser").Options;
			_userManager.When(u => u.DeleteAsync(Arg.Any<AppUser>())).Do(ci => { });
			using (var context = new AppDbContext(options))
			{
				var user = context.Users.Add(new AppUser("Sam", "Sample", "sam@sample.com", "test")).Entity;
				await context.SaveChangesAsync();
				var testObject = new DeleteUserActionHandler(context, _userManager);
				await testObject.Execute(new DeleteUserAction(new ActionCallContext(), user.Id));

				await _userManager.Received(1).DeleteAsync(Arg.Any<AppUser>());
			}
		}
	}
}
