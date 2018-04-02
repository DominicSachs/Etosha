using Etosha.Server.Common.Actions.RoleActions;
using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Etosha.Web.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Web.Api.Tests.Controller
{
    public sealed class RolesControllerTests
    {
        private readonly IActionExecutor _executor;
        private readonly RolesController _testObject;

        public RolesControllerTests()
        {
            _executor = Substitute.For<IActionExecutor>();
            var logger = Substitute.For<ILogger<RolesController>>();
            _testObject = new RolesController(new ClaimsPrincipal(), logger, _executor);
        }

        [Fact]
        public async Task Get_Should_Return_A_List_Of_Roles()
        {
            var roles = new[]
            {
                new UserRole(1, "Administrators"),
                new UserRole(2, "Users")
            };

            _executor.Execute(Arg.Any<ListRoleAction>()).Returns(new ListRoleActionResult(new ListRoleAction(null), roles));

            var result = await _testObject.Get();

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeOfType<UserRole[]>();
        }
    }
}
