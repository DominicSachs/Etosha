using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Etosha.Web.Api.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly ILogger<UsersController> _logger;
    private readonly IActionExecutor _actionExecutor;
    public UsersController(ILogger<UsersController> logger, IActionExecutor executor)
    {
      _logger = logger;
      _actionExecutor = executor;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var action = new ListUserAction(new ActionCallerContext());
      var result = await _actionExecutor.Execute(action);

      return Ok(result.Users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var action = new GetUserAction(new ActionCallerContext(), id);
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
      var action = new SaveUserAction(new ActionCallerContext(), user);
      var result = await _actionExecutor.Execute(action);

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]User user)
    {
      user.Id = id;
      var action = new SaveUserAction(new ActionCallerContext(), user);
      var result = await _actionExecutor.Execute(action);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var action = new DeleteUserAction(new ActionCallerContext(), id);
      await _actionExecutor.Execute(action);

      return NoContent();
    }
  }
}
