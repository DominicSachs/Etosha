using System.Threading.Tasks;
using Etosha.Server.ActionHandlers.UserActionHandlers;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
                context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com"));
                await context.SaveChangesAsync();

                var testObject = new ListUserActionHandler(context);
                var result = await testObject.Execute(new ListUserAction(new ActionCallerContext()));

                result.Users.Length.Should().Be(1);
            }
        }
    }
}
