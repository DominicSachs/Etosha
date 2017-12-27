using Etosha.Server.Common.Actions.Base;

namespace Etosha.Server.ActionHandlers.Base
{
	internal abstract class AbstractActionHandler
	{
		internal abstract string ActionName { get; }

		protected abstract AbstractActionResult ExecuteInternal(AbstractAction action);

		internal AbstractActionResult Execute(AbstractAction action)
		{
			AbstractActionResult actionResult = ExecuteInternal(action);

			return actionResult;
		}
	}

	internal abstract class AbstractActionHandler<TAction, TResult> : AbstractActionHandler where TAction : AbstractAction
																							where TResult : AbstractActionResult
	{
		internal sealed override string ActionName => typeof(TAction).Name;

		protected sealed override AbstractActionResult ExecuteInternal(AbstractAction action)
		{
			return ExecuteInternal(action as TAction);
		}

		protected abstract TResult ExecuteInternal(TAction action);
	}
}
