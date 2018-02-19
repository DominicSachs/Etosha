using Etosha.Server.Common.Models;
using Etosha.Server.Common.Validation;
using Etosha.Server.Entities;
using Etosha.Server.Providers.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Etosha.Server.Providers.Implementations
{
    internal class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Login(LoginModel model)
        {
            Require.ThatNotNull(model, nameof(model));

            var dbUser = await _userManager.FindByEmailAsync(model.Email);
            if (dbUser == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(dbUser, model.Password))
            {
                return new User(dbUser.Id, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.UserName);
            }

            return null;
        }
    }
}
