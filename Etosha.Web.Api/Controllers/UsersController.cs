using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
    public async Task<IEnumerable<User>> Get()
    {
      _logger.LogInformation("Getting items");

      var action = new ListUserAction(new ActionCallerContext());
      var result = await _actionExecutor.Execute(action);

      return result.Users;
    }

    [HttpGet("{id}")]
    public async Task<User> Get(int id)
    {
      var action = new GetUserAction(new ActionCallerContext(), id);
      var result = await _actionExecutor.Execute(action);

      return result.User;
    }

    [HttpPost]
    public User Post([FromBody]User user)
    {
      return user;
    }

    [HttpPut("{id}")]
    public User Put(int id, [FromBody]User user)
    {
      return user;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      return Ok();
    }
  }
}
