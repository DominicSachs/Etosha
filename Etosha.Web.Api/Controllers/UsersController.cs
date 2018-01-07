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

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<User> Get(int id)
    {
      var action = new GetUserAction(new ActionCallerContext(), id);
      var result = await _actionExecutor.Execute(action);

      return result.User;
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
