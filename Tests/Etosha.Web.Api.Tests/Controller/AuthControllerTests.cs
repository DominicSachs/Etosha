using Etosha.Server.Common.Models;
using Etosha.Server.Providers.Interfaces;
using Etosha.Web.Api.Controllers;
using Etosha.Web.Api.Infrastructure.Security;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Web.Api.Tests.Controller
{
  public sealed class AuthControllerTests
  {
    private readonly IWebTokenBuilder _webTokenBuilder;
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly AuthController _testObject;

    public AuthControllerTests()
    {
      _webTokenBuilder = Substitute.For<IWebTokenBuilder>();
      _authenticationProvider = Substitute.For<IAuthenticationProvider>();
      _testObject = new AuthController(Substitute.For<ILogger<AuthController>>(), _webTokenBuilder, _authenticationProvider);
    }

    [Fact]
    public async Task Login_Should_Return_BadRequest()
    {
      _testObject.ModelState.AddModelError("email", "Email required");

      var result = await _testObject.Login(new LoginModel());
      result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Login_Should_Return_NotFound()
    {
      var model = new LoginModel() { Email = "sam@sample.com", Password = "a" };
      _authenticationProvider.Login(model).Returns((User)null);
      var result = await _testObject.Login(model);
      result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Login_Should_Return_Ok()
    {
      var model = new LoginModel() { Email = "sam@sample.com", Password = "a" };
      _authenticationProvider.Login(model).Returns(new User());
      _webTokenBuilder.GenerateToken(Arg.Any<User>()).Returns("theToken");
      var result = await _testObject.Login(model);
      result.Should().BeOfType<OkObjectResult>();
    }
  }
}
