using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
			var users = from u in _context.Users
						where u.Id == action.UserId
						select new User(u.Id, u.FirstName, u.LastName, u.Email, u.UserName);

			return new GetUserActionResult(action, await users.SingleOrDefaultAsync());
		}
	}
}
