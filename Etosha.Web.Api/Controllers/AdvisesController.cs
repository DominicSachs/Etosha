using Microsoft.AspNetCore.Mvc;
using System;

namespace Etosha.Web.Api.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class AdvisesController : Controller
  {
    [HttpGet("{id}")]
    public IActionResult GetAdvise(Guid id)
    {
      if (id != Guid.Empty)
      {
        return Ok(new
        {
          Name = "Beratung für Sam Sample",
          ProductCount = 4
        });
      }

      return BadRequest();
    }
  }
}
