using Etosha.Server.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Etosha.Web.Api.Controllers
{
  public abstract class BaseController : Controller
  {
    protected ActionCallContext _actionCallContext;

    public BaseController(ClaimsPrincipal claimsPrincipal)
    {
      _actionCallContext = new ActionCallContext(claimsPrincipal);
    }
  }
}
