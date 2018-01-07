using Etosha.Server.Common.Actions.Base;
using System.Threading.Tasks;

namespace Etosha.Server.ActionHandlers.Base
{
	internal abstract class AbstractActionHandler
	{
		internal abstract string ActionName { get; }
	}

	internal abstract class AbstractActionHandler<TResult> : AbstractActionHandler
		where TResult : AbstractActionResult
	{
		internal abstract Task<TResult> Execute(AbstractAction<TResult> action);
	}

	internal abstract class AbstractActionHandler<TAction, TResult> : AbstractActionHandler<TResult>
		where TAction : AbstractAction
		where TResult : AbstractActionResult
	{
		internal sealed override string ActionName => typeof(TAction).Name;

		internal sealed override Task<TResult> Execute(AbstractAction<TResult> action)
		{
			return ExecuteInternal(action as TAction);
		}
		protected abstract Task<TResult> ExecuteInternal(TAction action);
	}
}
