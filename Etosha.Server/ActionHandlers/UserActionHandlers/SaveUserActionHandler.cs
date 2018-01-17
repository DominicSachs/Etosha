using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
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

		public SaveUserActionHandler(AppDbContext appDbContext, UserManager<AppUser> userManager)
		{
			_context = appDbContext;
			_userManager = userManager;
		}

		protected override async Task<SaveUserActionResult> ExecuteInternal(SaveUserAction action)
		{
			var user = action.User;
			if (action.User.Id == 0)
			{
				var autoPassword = PasswordGenerator.GenerateRandomPassword();
				var newUser = new AppUser(user.Email, user.FirstName, user.LastName, user.Email);
				await _userManager.CreateAsync(newUser, autoPassword);
			}
			else
			{
				var dbUser = await _userManager.FindByIdAsync(user.Id.ToString());
				dbUser.FirstName = user.FirstName;
				dbUser.LastName = user.LastName;
				dbUser.Email = user.Email;

				await _userManager.UpdateAsync(dbUser);
			}

			var createdUser = from u in _context.Users
							  where u.Id == user.Id
							  select new User
							  {
								  Id = u.Id,
								  FirstName = u.FirstName,
								  LastName = u.LastName,
								  Email = u.Email,
								  UserName = u.UserName
							  };

			return new SaveUserActionResult(action, await createdUser.SingleOrDefaultAsync());
		}
	}
}
