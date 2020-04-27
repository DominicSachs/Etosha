using System.Linq;
using System.Threading.Tasks;
using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using Etosha.Server.Specifications.UserSpecifications;
using Microsoft.AspNetCore.Identity;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
	internal class DeleteUserActionHandler : AbstractActionHandler<DeleteUserAction, DeleteUserActionResult>
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		public DeleteUserActionHandler(AppDbContext appDbContext, UserManager<AppUser> userManager)
		{
		    _context = appDbContext;
		    _userManager = userManager;
		}

		protected override async Task<DeleteUserActionResult> ExecuteInternal(DeleteUserAction action)
		{
			var user = _context.Users.Single(new UserIdSpecification(action.UserId).ToExpression());
			await _userManager.DeleteAsync(user);

			return new DeleteUserActionResult(action);
		}
	}
}
