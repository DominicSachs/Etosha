using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Common.Validation;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using Etosha.Server.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
    internal class SaveUserActionHandler : AbstractActionHandler<SaveUserAction, SaveUserActionResult>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public SaveUserActionHandler(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task<SaveUserActionResult> ExecuteInternal(SaveUserAction action)
        {
            Require.ThatNotNull(action.User, nameof(action.User));

            var user = action.User;
            var dbUser = default(AppUser);

            if (action.User.Id == 0)
            {
                var autoPassword = PasswordGenerator.GenerateRandomPassword();
                dbUser = new AppUser(user.Email, user.FirstName, user.LastName, user.Email);
                await _userManager.CreateAsync(dbUser, autoPassword);
                action.User.Id = dbUser.Id;
            }
            else
            {
                dbUser = await _userManager.FindByIdAsync(user.Id.ToString());
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.Email = user.Email;

                await _userManager.UpdateAsync(dbUser);
            }

            await SetUserRole(dbUser, user.RoleId);

            var createdUser = from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              where u.Id == user.Id
                              select new User(u.Id, u.FirstName, u.LastName, u.Email, u.UserName, ur.RoleId);

            return new SaveUserActionResult(action, await createdUser.SingleOrDefaultAsync());
        }

        private async Task SetUserRole(AppUser user, int roleId)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var newRole = await _roleManager.FindByIdAsync(roleId.ToString());

            if (!userRoles.Contains(newRole.Name))
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }
        }
    }
}
