using Etosha.Server.Common.Models;
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

    public AuthController(IWebTokenBuilder webTokenBuilder)
    {
      _webTokenBuilder = webTokenBuilder;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      string token = _webTokenBuilder.GenerateToken(new User(1, "Sam", "Sample", "sam@sample.com", "sam"));
      return Ok(token);
    }
  }
}
