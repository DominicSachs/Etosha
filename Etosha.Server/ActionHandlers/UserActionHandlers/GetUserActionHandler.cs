using System.Linq;
using System.Threading.Tasks;
using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.EntityFramework;
using Etosha.Server.Specifications.UserSpecifications;
using Microsoft.EntityFrameworkCore;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
	internal class GetUserActionHander : AbstractActionHandler<GetUserAction, GetUserActionResult>
	{
		private readonly AppDbContext _context;

		public GetUserActionHander(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}

		protected override async Task<GetUserActionResult> ExecuteInternal(GetUserAction action)
		{
			var user = await _context.Users
								.Where(new UserIdSpecification(action.UserId).ToExpression())
								.Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
								.Select(u => new User(u.User.Id, u.User.FirstName, u.User.LastName, u.User.Email, u.User.UserName, u.UserRole.RoleId))
								.SingleOrDefaultAsync();

			return new GetUserActionResult(action, user);
		}
	}
}
