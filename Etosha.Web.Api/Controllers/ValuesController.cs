using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Execution;
using Etosha.Server.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Etosha.Web.Api.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
    private readonly ILogger<ValuesController> _logger;
    private readonly IActionExecutor _actionExecutor;
    public ValuesController(ILogger<ValuesController> logger, IActionExecutor executor)
    {
      _logger = logger;
      _actionExecutor = executor;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
      _logger.LogInformation("Getting items");

      var action = new ListUserAction(new ActionCallerContext());
      var result = _actionExecutor.Execute<ListUserAction, ListUserActionResult>(action);

      return result.Users;
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      _logger.LogInformation($"Get item by id: {id}");
      return "value";
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
