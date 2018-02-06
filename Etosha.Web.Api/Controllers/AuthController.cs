using Etosha.Server.Common.Models;
using Etosha.Server.Providers.Interfaces;
using Etosha.Web.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Etosha.Web.Api.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IWebTokenBuilder _webTokenBuilder;
    private readonly IAuthenticationProvider _authenticationProvider;

    public AuthController(IWebTokenBuilder webTokenBuilder, IAuthenticationProvider authenticationProvider)
    {
      _webTokenBuilder = webTokenBuilder;
      _authenticationProvider = authenticationProvider;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _authenticationProvider.Login(model);

      if (user == null)
      {
        return NotFound();
      }

      string token = _webTokenBuilder.GenerateToken(new User(1, "Sam", "Sample", "sam@sample.com", "sam"));
      return Ok(token);
    }
  }
}
