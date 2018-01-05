using Etosha.Server.Common.Actions.Base;

namespace Etosha.Server.ActionHandlers.Base
{
	internal abstract class AbstractActionHandler
	{
		internal abstract string ActionName { get; }
	}

    internal abstract class AbstractActionHandler<TResult> : AbstractActionHandler
        where TResult : AbstractActionResult
    {
        internal abstract TResult Execute(AbstractAction<TResult> action);
    }

	internal abstract class AbstractActionHandler<TAction, TResult> : AbstractActionHandler<TResult>
	    where TAction : AbstractAction
		where TResult : AbstractActionResult
	{
		internal sealed override string ActionName => typeof(TAction).Name;

	    internal sealed override TResult Execute(AbstractAction<TResult> action)
	    {
	        return InternalExecute(action as TAction);
	    }
	    protected abstract TResult InternalExecute(TAction action);
	}
}
