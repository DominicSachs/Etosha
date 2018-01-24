using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
	internal class DeleteUserActionHandler : AbstractActionHandler<DeleteUserAction, DeleteUserActionResult>
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		public DeleteUserActionHandler(AppDbContext appDbContext, UserManager<AppUser> userManager)
		{
			_context = appDbContext;
		}

		protected override async Task<DeleteUserActionResult> ExecuteInternal(DeleteUserAction action)
		{
			var user = from u in _context.Users
					   where u.Id == action.UserId
					   select u;

			await _userManager.DeleteAsync(user.Single());
			return new DeleteUserActionResult(action);
		}
	}
}
