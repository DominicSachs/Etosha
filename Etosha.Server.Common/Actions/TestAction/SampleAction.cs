using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.TestAction
{
    public class SampleAction : AbstractAction<SampleActionResult>
    {
        public SampleAction(ActionCallerContext context) : base(context) { }
    }

    public class SampleActionResult : AbstractActionResult<SampleAction>
    {
        public SampleActionResult(SampleAction action) : base(action) { }

        public string Test { get; set; }
    }
}
