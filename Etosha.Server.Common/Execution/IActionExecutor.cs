using Etosha.Server.Common.Actions.Base;

namespace Etosha.Server.Common.Execution
{
    public interface IActionExecutor
    {
        TResult Execute<TResult>(AbstractAction<TResult> action) where TResult : AbstractActionResult;
    }
}
