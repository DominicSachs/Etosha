using System.Threading.Tasks;
using Etosha.Server.Common.Actions.Base;

namespace Etosha.Server.Common.Execution
{
    public interface IActionExecutor
    {
        Task<TResult> Execute<TResult>(AbstractAction<TResult> action)
            where TResult : AbstractActionResult;
    }
}
