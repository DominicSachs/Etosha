using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.TestAction;

namespace Etosha.Server.ActionHandlers.TestActionHandlers
{
    internal class SampleActionHandler : AbstractActionHandler<SampleAction, SampleActionResult>
    {
        protected override SampleActionResult ExecuteInternal(SampleAction action)
        {
            return new SampleActionResult(action)
            {
                Test = $"Hello from {nameof(SampleActionHandler)}"
            };
        }
    }
}
