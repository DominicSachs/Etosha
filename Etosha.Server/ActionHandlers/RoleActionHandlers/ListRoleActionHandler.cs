using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.RoleActions;
using Etosha.Server.Common.Models;
using Etosha.Server.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Etosha.Server.ActionHandlers.RoleActionHandlers
{
    internal class ListRoleActionHandler : AbstractActionHandler<ListRoleAction, ListRoleActionResult>
    {
        private readonly AppDbContext _context;

        public ListRoleActionHandler(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        protected override async Task<ListRoleActionResult> ExecuteInternal(ListRoleAction action)
        {
            var roles = from r in _context.Roles
                        select new UserRole(r.Id, r.Name);

            return new ListRoleActionResult(action, await roles.ToArrayAsync());
        }
    }
}
