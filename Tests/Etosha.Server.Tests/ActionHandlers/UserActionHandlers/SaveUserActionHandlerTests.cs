using Etosha.Server.ActionHandlers.UserActionHandlers;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Etosha.Server.Tests.ActionHandlers.UserActionHandlers
{
    public sealed class SaveUserActionHandlerTests
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public SaveUserActionHandlerTests()
        {
            var userStore = Substitute.For<IUserStore<AppUser>>();
            _userManager = Substitute.For<UserManager<AppUser>>(
                userStore,
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<AppUser>>(),
                Enumerable.Empty<IUserValidator<AppUser>>(),
                Enumerable.Empty<IPasswordValidator<AppUser>>(),
                Substitute.For<ILookupNormalizer>(),
                new IdentityErrorDescriber(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<AppUser>>>());

            var roleStore = Substitute.For<IRoleStore<AppRole>>();

            _roleManager = Substitute.For<RoleManager<AppRole>>(
                roleStore,
                Substitute.For<IEnumerable<IRoleValidator<AppRole>>>(),
                Substitute.For<ILookupNormalizer>(),
                new IdentityErrorDescriber(),
                Substitute.For<ILogger<RoleManager<AppRole>>>());
        }

        [Fact]
        public async Task Should_Create_A_User()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("SaveUser").Options;
            _userManager.When(u => u.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>())).Do(ci =>
            {
                var userParam = ci.ArgAt<AppUser>(0);
                userParam.Id = 1;
            });

            using (var context = new AppDbContext(options))
            {
                var role = context.Roles.Add(new AppRole("Administrators")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = 1, RoleId = role.Id });

                await context.SaveChangesAsync();
                var user = new User(0, "Sam", "Sample", "sam@sample.com", "test");
                var testObject = new SaveUserActionHandler(context, _userManager, _roleManager);
                var result = await testObject.Execute(new SaveUserAction(new ActionCallContext(), user));

                await _userManager.Received(1).CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>());
                result.Id.Should().Be(1);
            }
        }

        [Fact]
        public async Task Should_Update_A_User()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("UpdateUser").Options;
            _userManager.When(u => u.UpdateAsync(Arg.Any<AppUser>())).Do(ci => { });
            var dbUser = new AppUser("test", "Sam", "Sample", "sam@sample.com") { Id = 1 };
            _userManager.FindByIdAsync("1").Returns(dbUser);

            using (var context = new AppDbContext(options))
            {
                var user = new User(1, "Ela", "Example", "ela@example.com", "test");
                var role = context.Roles.Add(new AppRole("Administrators")).Entity;
                context.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role.Id });

                var testObject = new SaveUserActionHandler(context, _userManager, _roleManager);
                await testObject.Execute(new SaveUserAction(new ActionCallContext(), user));

                dbUser.FirstName = "Ela";
                dbUser.LastName = "Example";
                dbUser.Email = "ela@example.com";
                await _userManager.Received(1).UpdateAsync(dbUser);
            }
        }
    }
}
