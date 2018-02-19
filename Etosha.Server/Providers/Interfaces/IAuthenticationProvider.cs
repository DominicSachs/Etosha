using Etosha.Server.Common.Models;
using System.Threading.Tasks;

namespace Etosha.Server.Providers.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<User> Login(LoginModel model);
    }
}
