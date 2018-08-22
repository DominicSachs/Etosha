using Etosha.Server.ActionHandlers.RoleActionHandlers;
using Etosha.Server.Common.Actions.RoleActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Server.Tests.ActionHandlers.UserActionHandlers
{
    public sealed class ListRoleActionHandlerTests
    {
        [Fact]
        public async Task Should_Return_A_List_Of_Roles()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("Roles").Options;

            using (var context = new AppDbContext(options))
            {
                context.Roles.Add(new AppRole("Administrators"));
                context.Roles.Add(new AppRole("Users"));
                await context.SaveChangesAsync();

                var testObject = new ListRoleActionHandler(context);
                var result = await testObject.Execute(new ListRoleAction(new ActionCallContext()));

                result.Roles.Length.Should().Be(2);
            }
        }
    }
}
