using System;
using Etosha.Server.Common.Models;

namespace Etosha.Server.Common.Actions.Base
{
    public abstract class AbstractAction
    {
        public AbstractAction(ActionCallContext context)
        {
            Id = Guid.NewGuid();
            ActionCallContext = context;
        }

        public string Name => GetType().Name;

        public Guid Id { get; }

        public ActionCallContext ActionCallContext { get; set; }
    }

    public abstract class AbstractAction<TResult> : AbstractAction where TResult : AbstractActionResult
    {
        public AbstractAction(ActionCallContext context) : base(context) { }
    }
}
