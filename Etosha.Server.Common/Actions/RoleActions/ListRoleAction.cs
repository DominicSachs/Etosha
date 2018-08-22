using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.RoleActions
{
    public class ListRoleAction : AbstractAction<ListRoleActionResult>
    {
        public ListRoleAction(ActionCallContext context) : base(context) { }
    }

    public class ListRoleActionResult : AbstractActionResult<ListRoleAction>
    {
        public ListRoleActionResult(ListRoleAction action, UserRole[] roles) : base(action)
        {
            Roles = roles;
        }

        public UserRole[] Roles { get; }
    }
}
