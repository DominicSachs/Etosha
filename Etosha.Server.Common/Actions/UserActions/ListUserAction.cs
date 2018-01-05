using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.UserActions
{
	public class ListUserAction : AbstractAction<ListUserActionResult>
	{
		public ListUserAction(ActionCallerContext context) : base(context) { }
	}

	public class ListUserActionResult : AbstractActionResult<ListUserAction>
	{
		public ListUserActionResult(ListUserAction action, User[] users) : base(action)
		{
			Users = users;
		}

		public User[] Users { get; }
	}
}
