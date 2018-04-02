using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Common.Validation;
using Etosha.Server.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
    internal class ListUserActionHandler : AbstractActionHandler<ListUserAction, ListUserActionResult>
    {
        private readonly AppDbContext _context;

        public ListUserActionHandler(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        protected override async Task<ListUserActionResult> ExecuteInternal(ListUserAction action)
        {
            Require.ThatNotNull(action.ActionCallContext, nameof(action.ActionCallContext));

            var users = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        where u.Id != action.ActionCallContext.UserId
                        select new User
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            UserName = u.UserName,
                            RoleId = ur.RoleId
                        };

            return new ListUserActionResult(action, await users.ToArrayAsync());
        }
    }
}
