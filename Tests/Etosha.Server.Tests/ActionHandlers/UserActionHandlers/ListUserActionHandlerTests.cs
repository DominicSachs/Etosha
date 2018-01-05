using System;
using System.Linq;
using Etosha.Server.Common.Actions.UserActions;
using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Etosha.Server.ActionHandlers.UserActionHandlers
{
    public sealed class ListUserActionHandlerTests : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly ListUserActionHandler _actionHandler;
        private readonly AppDbContext _context;

        public ListUserActionHandlerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ReadUsers")
                .Options;
            _context = new AppDbContext(_dbContextOptions);
            _actionHandler = new ListUserActionHandler(_context);
        }

        [Fact]
        public void Should_Return_A_List_Of_Users()
        {
            _context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com"));
            _context.SaveChanges();

            var result = _actionHandler.Execute(new ListUserAction(new ActionCallerContext()));

            result.Should().BeOfType<ListUserActionResult>();
            ((ListUserActionResult)result).Users.Length.Should().Be(1);
        }

        public void Dispose()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var users = context.Users.ToList();
                context.Users.RemoveRange(users);
                context.SaveChanges();
            }
        }
    }
}
