using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.EntityFramework;
using System.Linq;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
	internal class ListUserActionHandler : AbstractActionHandler<ListUserAction, ListUserActionResult>
	{
		private readonly AppDbContext _context;

		public ListUserActionHandler(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}

		protected override ListUserActionResult InternalExecute(ListUserAction action)
		{
			var users = from u in _context.Users
						select new User
						{
							Id = u.Id,
							FirstName = u.FirstName,
							LastName = u.LastName,
							Email = u.Email,
							UserName = u.UserName
						};

			return new ListUserActionResult(action, users.ToArray());
		}
	}
}
