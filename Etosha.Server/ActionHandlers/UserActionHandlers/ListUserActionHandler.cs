using System.Linq;
using System.Threading.Tasks;
using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Common.Validation;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using Etosha.Server.Specifications;
using Etosha.Server.Specifications.UserSpecifications;
using Microsoft.EntityFrameworkCore;

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

            var specification = new NotSpecification<AppUser>(new UserIdSpecification(action.ActionCallContext.UserId));

            var users = await _context.Users
                .Where(specification.ToExpression())
                .Select(u => new User(u.Id, u.FirstName, u.LastName, u.Email, u.UserName))
                .ToArrayAsync();

            return new ListUserActionResult(action, users);
        }
    }
}
