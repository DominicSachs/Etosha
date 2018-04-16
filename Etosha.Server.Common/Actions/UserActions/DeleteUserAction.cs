using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.UserActions
{
	public class DeleteUserAction : AbstractAction<DeleteUserActionResult>
	{
		public DeleteUserAction(ActionCallContext context, int id) : base(context)
		{
			UserId = id;
		}

		public int UserId { get; }
	}

	public class DeleteUserActionResult : AbstractActionResult<DeleteUserAction>
	{
		public DeleteUserActionResult(DeleteUserAction action) : base(action) { }
	}
}
