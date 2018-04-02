using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Etosha.Web.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class UsersController : BaseController
  {
    private readonly ILogger<UsersController> _logger;
    private readonly IActionExecutor _actionExecutor;

    public UsersController(ClaimsPrincipal claimsPrincipal, ILogger<UsersController> logger, IActionExecutor executor) : base(claimsPrincipal)
    {
      _logger = logger;
      _actionExecutor = executor;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var action = new ListUserAction(_actionCallContext);
      var result = await _actionExecutor.Execute(action);

      return Ok(result.Users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var action = new GetUserAction(_actionCallContext, id);
      var result = await _actionExecutor.Execute(action);

      if (result.User == null)
      {
        return NotFound();
      }

      return Ok(result.User);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]User user)
    {
      var action = new SaveUserAction(_actionCallContext, user);
      await _actionExecutor.Execute(action);

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]User user)
    {
      user.Id = id;
      var action = new SaveUserAction(_actionCallContext, user);
      await _actionExecutor.Execute(action);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var action = new DeleteUserAction(_actionCallContext, id);
      await _actionExecutor.Execute(action);

      return NoContent();
    }
  }
}
