using Etosha.Server.Common.Actions.Base;

namespace Etosha.Server.Common.Execution
{
    public interface IActionExecutor
    {
        TResult Execute<TAction, TResult>(TAction action)
            where TAction : AbstractAction
            where TResult : AbstractActionResult;
    }
}
