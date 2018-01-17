using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.UserActions
{
	public class SaveUserAction : AbstractAction<GetUserActionResult>
	{
		public SaveUserAction(ActionCallerContext context, User user) : base(context)
		{
			User = user;
		}

		public User User { get; }
	}

	public class SaveUserActionResult : AbstractActionResult<SaveUserAction>
	{
		public SaveUserActionResult(SaveUserAction action, User user) : base(action)
		{
			User = user;
		}

		public User User { get; }
	}
}
