using Etosha.Server.Common.Actions.RoleActions;
using Etosha.Server.Common.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Etosha.Web.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class RolesController : BaseController
  {
    private readonly ILogger<RolesController> _logger;
    private readonly IActionExecutor _actionExecutor;

    public RolesController(ClaimsPrincipal claimsPrincipal, ILogger<RolesController> logger, IActionExecutor executor) : base(claimsPrincipal)
    {
      _logger = logger;
      _actionExecutor = executor;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var action = new ListRoleAction(_actionCallContext);
      var result = await _actionExecutor.Execute(action);

      return Ok(result.Roles);
    }
  }
}
