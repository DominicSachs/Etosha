using Etosha.Server.Common.Models;
using Etosha.Server.Providers.Interfaces;
using Etosha.Web.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Etosha.Web.Api.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IWebTokenBuilder _webTokenBuilder;
    private readonly IAuthenticationProvider _authenticationProvider;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, IWebTokenBuilder webTokenBuilder, IAuthenticationProvider authenticationProvider)
    {
      _logger = logger;
      _webTokenBuilder = webTokenBuilder;
      _authenticationProvider = authenticationProvider;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogError($"Invalid model for user: ${model?.Email} and password with length ${model?.Password?.Length ?? 0}");
        return BadRequest(ModelState);
      }

      var user = await _authenticationProvider.Login(model);

      if (user == null)
      {
        _logger.LogError($"User not found for user: ${model?.Email} and password with length ${model?.Password?.Length ?? 0}");
        return BadRequest();
      }

      string token = _webTokenBuilder.GenerateToken(user);
      return Ok(new { user.FirstName, user.LastName, Token = token });
    }
  }
}
