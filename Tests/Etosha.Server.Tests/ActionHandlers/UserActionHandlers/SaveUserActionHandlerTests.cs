using Etosha.Server.ActionHandlers.UserActionHandlers;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
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
	public sealed class SaveUserActionHandlerTests
	{
		private UserManager<AppUser> _userManager;

		public SaveUserActionHandlerTests()
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
		public async Task Should_Create_A_User()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("SaveUser").Options;
#pragma warning disable 4014
			_userManager.When(u => u.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>())).Do(ci =>
#pragma warning restore 4014
			{
				var userParam = ci.ArgAt<AppUser>(0);
				userParam.Id = 1;
			});

			using (var context = new AppDbContext(options))
			{
				context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com") { Id = 1 });
				await context.SaveChangesAsync();
				var user = new User(0, "Sam", "Sample", "sam@sample.com", "test");
				var testObject = new SaveUserActionHandler(context, _userManager);
				var result = await testObject.Execute(new SaveUserAction(new ActionCallerContext(), user));

				await _userManager.Received(1).CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>());
				result.User.FirstName.Should().Be(user.FirstName);
				result.User.LastName.Should().Be(user.LastName);
				result.User.Email.Should().Be(user.Email);
				result.User.UserName.Should().Be(user.UserName);
			}
		}

		[Fact]
		public async Task Should_Update_A_User()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("UpdateUser").Options;
			_userManager.When(u => u.UpdateAsync(Arg.Any<AppUser>())).Do(ci => { });
			var dbUser = new AppUser("test", "Sam", "Sample", "sam@sample.com") { Id = 1 };
			_userManager.FindByIdAsync("1").Returns(dbUser);

			using (var context = new AppDbContext(options))
			{
				var user = new User(1, "Ela", "Example", "ela@example.com", "test");
				var testObject = new SaveUserActionHandler(context, _userManager);
				await testObject.Execute(new SaveUserAction(new ActionCallerContext(), user));

				dbUser.FirstName = "Ela";
				dbUser.LastName = "Example";
				dbUser.Email = "ela@example.com";
				await _userManager.Received(1).UpdateAsync(dbUser);
			}
		}
	}
}
