using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Etosha.Web.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Web.Api.Tests.Controller
{
	public sealed class UsersControllerTests
	{
		private readonly IActionExecutor _executor;
	    private readonly UsersController _testObject;

		public UsersControllerTests()
		{
		    _executor = Substitute.For<IActionExecutor>();
			var logger = Substitute.For<ILogger<UsersController>>();
			_testObject = new UsersController(logger, _executor);
		}

		[Fact]
		public async Task Get_Should_Return_A_List_Of_User()
		{
			var users = new[]
			{
				new User(1, "Sam", "Sample", "sam@sample.com", "sam"),
				new User(2, "Ema", "Example", "ema@example.com", "ema")
			};

			_executor.Execute(Arg.Any<ListUserAction>()).Returns(new ListUserActionResult(new ListUserAction(null), users));

			var result = await _testObject.Get();

			result.Should().BeOfType<OkObjectResult>();
			((OkObjectResult)result).Value.Should().BeOfType<User[]>();
		}

		[Fact]
		public async Task Get_Should_Return_A_NotFound()
		{
			_executor.Execute(Arg.Any<GetUserAction>()).Returns(new GetUserActionResult(new GetUserAction(null, 1), null));

			var result = await _testObject.Get(1);

			result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task Get_Should_Return_A_User()
		{
			var user = new User(1, "Sam", "Sample", "sam@sample.com", "sam");

			_executor.Execute(Arg.Any<GetUserAction>()).Returns(new GetUserActionResult(new GetUserAction(null, 1), user));

			var result = await _testObject.Get(1);

			result.Should().BeOfType<OkObjectResult>();
			((OkObjectResult)result).Value.Should().BeOfType<User>();
			((User)((OkObjectResult)result).Value).Should().BeEquivalentTo(user);
		}

		[Fact]
		public async Task Post_Should_Return_A_NoContentResult()
		{
			var user = new User(0, "Sam", "Sample", "sam@sample.com", "sam");

			_executor.Execute(Arg.Any<SaveUserAction>()).Returns(new SaveUserActionResult(new SaveUserAction(null, user), user));

			var result = await _testObject.Post(user);

			result.Should().BeOfType<NoContentResult>();
		}

		[Fact]
		public async Task Put_Should_Return_A_NoContentResult()
		{
			var user = new User(1, "Sam", "Sample", "sam@sample.com", "sam");

			_executor.Execute(Arg.Any<SaveUserAction>()).Returns(new SaveUserActionResult(new SaveUserAction(null, user), user));

			var result = await _testObject.Put(1, user);

			result.Should().BeOfType<NoContentResult>();
		}

		[Fact]
		public async Task Delete_Should_Return_A_NoContentResult()
		{
			_executor.Execute(Arg.Any<DeleteUserAction>()).Returns(new DeleteUserActionResult(new DeleteUserAction(null, 1)));

			var result = await _testObject.Delete(1);

			result.Should().BeOfType<NoContentResult>();
		}
	}
}
