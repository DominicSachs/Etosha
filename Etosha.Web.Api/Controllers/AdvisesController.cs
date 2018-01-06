using Microsoft.AspNetCore.Mvc;

namespace Etosha.Web.Api.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class AdvisesController : Controller
  {
    [HttpGet("{id}")]
    public IActionResult GetAdvise(int id)
    {
      if (id == 0)
      {
        return BadRequest();
      }

      return Ok(new
      {
        Name = "Beratung für Sam Sample",
        ProductCount = 4
      });
    }
  }
}
