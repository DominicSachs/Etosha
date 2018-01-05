using Etosha.Server.Common.Actions.Base;

namespace Etosha.Server.ActionHandlers.Base
{
	internal abstract class AbstractActionHandler
	{
		internal abstract string ActionName { get; }
	}

	internal abstract class AbstractActionHandler<TAction, TResult> : AbstractActionHandler where TAction : AbstractAction
																							where TResult : AbstractActionResult
	{
		internal sealed override string ActionName => typeof(TAction).Name;

	    internal abstract TResult Execute(TAction action);
	}
}
