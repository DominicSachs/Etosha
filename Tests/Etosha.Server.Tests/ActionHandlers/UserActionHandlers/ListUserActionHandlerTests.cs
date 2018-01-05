using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
	public sealed class ListUserActionHandlerTests
	{
		[Fact]
		public void Should_Return_A_List_Of_Users()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "ReadUsers")
				.Options;

			using (var context = new AppDbContext(options))
			{
				context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com"));
				context.SaveChanges();
			}

			using (var context = new AppDbContext(options))
			{
				var actionHandler = new ListUserActionHandler(context);
				var result = actionHandler.Execute(new ListUserAction(new ActionCallerContext()));

				result.Should().BeOfType<ListUserActionResult>();
				((ListUserActionResult)result).Users.Length.Should().Be(1);
			}
		}
	}
}
