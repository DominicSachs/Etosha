using System.Threading.Tasks;
using Etosha.Server.Common.Execution;
using Etosha.Web.Api.Controllers;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Etosha.Web.Api.Tests.Controller
{
    public sealed class UsersControllerTests
    {
        private readonly IActionExecutor _executor;
        private readonly ILogger<UsersController> _logger;
        private readonly UsersController _testObject;

        public UsersControllerTests()
        {
            _executor = Substitute.For<IActionExecutor>();
            _logger = Substitute.For<ILogger<UsersController>>();
            _testObject = new UsersController(_logger, _executor);
        }

        public async Task Should_Return_A_User()
        {

        }
        //[HttpGet]
        //public async Task<IEnumerable<User>> Get()
        //{
        //  _logger.LogInformation("Getting items");

        //  var action = new ListUserAction(new ActionCallerContext());
        //  var result = await _actionExecutor.Execute(action);

        //  return result.Users;
        //}

        //[HttpGet("{id}")]
        //public async Task<User> Get(int id)
        //{
        //  var action = new GetUserAction(new ActionCallerContext(), id);
        //  var result = await _actionExecutor.Execute(action);

        //  return result.User;
        //}

        //[HttpPost]
        //public async Task<User> Post([FromBody]User user)
        //{
        //  var action = new SaveUserAction(new ActionCallerContext(), user);
        //  var result = await _actionExecutor.Execute(action);

        //  return result.User;
        //}

        //[HttpPut("{id}")]
        //public async Task<User> Put(int id, [FromBody]User user)
        //{
        //  user.Id = id;
        //  var action = new SaveUserAction(new ActionCallerContext(), user);
        //  var result = await _actionExecutor.Execute(action);

        //  return result.User;
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //  var action = new DeleteUserAction(new ActionCallerContext(), id);
        //  await _actionExecutor.Execute(action);

        //  return Ok();
        //}
    }
}
