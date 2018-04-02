using Etosha.Server.ActionHandlers.UserActionHandlers;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Server.Tests.ActionHandlers.UserActionHandlers
{
    public sealed class GetUserActionHandlerTests
    {
        [Fact]
        public async Task Should_Return_A_User()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("GetUser").Options;

            using (var context = new AppDbContext(options))
            {
                var user = context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com")).Entity;
                var role = context.Roles.Add(new AppRole("Administrators")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role.Id });
                await context.SaveChangesAsync();

                var testObject = new GetUserActionHander(context);
                var result = await testObject.Execute(new GetUserAction(new ActionCallContext(), user.Id));

                result.User.FirstName.Should().Be("Sam");
                result.User.LastName.Should().Be("Sample");
                result.User.Email.Should().Be("sam@sample.com");
            }
        }

        [Fact]
        public async Task Should_Return_Null_Without_An_Exception_If_User_Not_Found()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("GetUserNull").Options;

            using (var context = new AppDbContext(options))
            {
                var user = context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com")).Entity;
                var role = context.Roles.Add(new AppRole("Administrators")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role.Id });
                await context.SaveChangesAsync();

                var testObject = new GetUserActionHander(context);
                var result = await testObject.Execute(new GetUserAction(new ActionCallContext(), -1));

                result.User.Should().BeNull();
            }
        }
    }
}
