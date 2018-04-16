using Etosha.Server.ActionHandlers.UserActionHandlers;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Server.Tests.ActionHandlers.UserActionHandlers
{
    public sealed class ListUserActionHandlerTests
    {
        [Fact]
        public async Task Should_Return_A_List_Of_Users()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("Users").Options;

            using (var context = new AppDbContext(options))
            {
                var user = context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com")).Entity;
                var role = context.Roles.Add(new AppRole("Administrators")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role.Id });
                await context.SaveChangesAsync();

                var testObject = new ListUserActionHandler(context);
                var result = await testObject.Execute(new ListUserAction(new ActionCallContext()));

                result.Users.Length.Should().Be(1);
            }
        }

        [Fact]
        public async Task Should_Return_A_List_Of_Users_Without_The_Logged_In_User()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("Users2").Options;

            using (var context = new AppDbContext(options))
            {
                var currentUser = new AppUser("test", "Sam", "Sample", "sam@sample.com");
                context.Users.Add(currentUser);
                var role = context.Roles.Add(new AppRole("Administrators")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = currentUser.Id, RoleId = role.Id });
                var user = context.Users.Add(new AppUser("emil", "Emil", "Example", "emil@example.com")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role.Id });
                await context.SaveChangesAsync();

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, "username"),
                    new Claim(ClaimTypes.Email, "test@test.com"),
                };
                var identity = new ClaimsIdentity(claims, "TestAuthType");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var testObject = new ListUserActionHandler(context);

                var result = await testObject.Execute(new ListUserAction(new ActionCallContext(claimsPrincipal)));

                result.Users.Length.Should().Be(1);
            }
        }
    }
}
