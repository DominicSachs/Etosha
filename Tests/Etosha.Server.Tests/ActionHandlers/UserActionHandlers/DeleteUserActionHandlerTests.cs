namespace Etosha.Server.Tests.ActionHandlers.UserActionHandlers
{
    //public sealed class DeleteUserActionHandlerTests
    //{
        //[Fact]
        //public async Task Should_Delete_A_User()
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DeleteUser").Options;

        //    var userStore = Substitute.For<IUserStore<AppUser>>();
        //    using (var context = new AppDbContext(options))
        //    {
        //        var user = context.Users.Add(new AppUser("test", "Sam", "Sample", "sam@sample.com")).Entity;
        //        var userManager = new UserManager<AppUser>(userStore, null, null, null, null, null, null, null, null);
        //        await context.SaveChangesAsync();

        //        var testObject = new DeleteUserActionHandler(context, userManager);
        //        await testObject.Execute(new DeleteUserAction(new ActionCallerContext(), user.Id));

        //        await userManager.DeleteAsync(Arg.Any<AppUser>()).Received(1);
        //    }
        //}

        //[Fact]
        //public async Task Should_Return_Null_Without_An_Exception_If_User_Not_Found()
        //{
        //    var result = await _testObject.Execute(new GetUserAction(new ActionCallerContext(), -1));

        //    result.User.Should().BeNull();
        //}

        //public void Dispose()
        //{
        //    using (var context = new AppDbContext(_dbContextOptions))
        //    {
        //        var users = context.Users.ToList();
        //        context.Users.RemoveRange(users);
        //        context.SaveChanges();
        //    }
        //}
    //}
}
