using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Execution;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Etosha.Server.Execution
{
    internal class ActionExecutor : IActionExecutor
    {
        private readonly ILogger<IActionExecutor> _logger;
        private static ActionHandlers _actionHandlers;
        protected readonly IServiceProvider _serviceProvider;

        public ActionExecutor(IServiceProvider serviceProvider, ILogger<ActionExecutor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            if (_actionHandlers == null)
            {
                _actionHandlers = new ActionHandlers();

                var baseType = typeof(AbstractActionHandler);
                foreach (var type in baseType.Assembly.GetTypes().Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t)))
                {
                    _actionHandlers.Register((AbstractActionHandler)Activator.CreateInstance(type));
                }
            }
        }

        public TResult Execute<TResult>(AbstractAction<TResult> action) where TResult : AbstractActionResult
        {
            var handler = _actionHandlers.Find(action);

            _logger.LogDebug($"Start executing handler for action {action.Name}");

            var result = handler.Execute(action) as TResult;

            _logger.LogDebug($"End executing handler for action {action.Name} with result {JsonConvert.SerializeObject(result)}");

            return result;
        }
    }

    internal class ActionHandlers
    {
        private Dictionary<string, AbstractActionHandler> _items;

        #region Ctor
        internal ActionHandlers()
        {
            _items = new Dictionary<string, AbstractActionHandler>();
        }
        #endregion

        #region Member
        internal AbstractActionHandler Find(AbstractAction action)
        {
            AbstractActionHandler result = null;

            _items.TryGetValue(action.Name, out result);

            return result;
        }

        internal void Register(AbstractActionHandler handler)
        {
            _items.Add(handler.ActionName, handler);
        }

        internal void Unregister(AbstractActionHandler handler)
        {
            _items.Remove(handler.ActionName);
        }
        #endregion
    }
}
