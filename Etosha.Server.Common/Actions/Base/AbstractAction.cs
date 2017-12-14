using Etosha.Server.Common.Models;
using System;

namespace Etosha.Server.Common.Actions.Base
{
    public abstract class AbstractAction
    {
        public AbstractAction(ActionCallerContext context)
        {
            Id = Guid.NewGuid();
        }

        public string Name => GetType().Name;

        public Guid Id { get; }
    }

    public abstract class AbstractAction<TResult> : AbstractAction where TResult : AbstractActionResult
    {
        public AbstractAction(ActionCallerContext context) : base(context) { }
    }
}
