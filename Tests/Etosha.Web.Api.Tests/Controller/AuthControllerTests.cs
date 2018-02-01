using Etosha.Server.Common.Models;
using Etosha.Web.Api.Controllers;
using Etosha.Web.Api.Infrastructure.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Web.Api.Tests.Controller
{
    public sealed class AuthControllerTests
    {
        private readonly IWebTokenBuilder _webTokenBuilder;
        private readonly AuthController _testObject;

        public AuthControllerTests()
        {
            _webTokenBuilder = Substitute.For<IWebTokenBuilder>();
            _testObject = new AuthController(_webTokenBuilder);
        }

        [Fact]
        public async Task Login_Should_Return_BadRequest()
        {
            _webTokenBuilder.GenerateToken(Arg.Any<User>()).Returns("theToken");
            var result = await _testObject.Login(new LoginModel());

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
