using Etosha.Server.Common.Models;

namespace Etosha.Web.Api.Infrastructure.Security
{
  public interface IWebTokenBuilder
  {
    string GenerateToken(User user);
  }
}
