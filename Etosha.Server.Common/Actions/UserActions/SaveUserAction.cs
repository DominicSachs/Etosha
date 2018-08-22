using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.UserActions
{
    public class SaveUserAction : AbstractAction<SaveUserActionResult>
    {
        public SaveUserAction(ActionCallContext context, User user) : base(context)
        {
            User = user;
        }

        public User User { get; }
    }

    public class SaveUserActionResult : AbstractActionResult<SaveUserAction>
    {
        public SaveUserActionResult(SaveUserAction action, int id) : base(action)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
