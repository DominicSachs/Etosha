using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.UserActions
{
	public class GetUserAction : AbstractAction<GetUserActionResult>
	{
		public GetUserAction(ActionCallContext context, int id) : base(context)
		{
			UserId = id;
		}

		public int UserId { get; }
	}

	public class GetUserActionResult : AbstractActionResult<GetUserAction>
	{
		public GetUserActionResult(GetUserAction action, User user) : base(action)
		{
			User = user;
		}

		public User User { get; }
	}
}
