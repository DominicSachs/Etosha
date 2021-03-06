﻿using Etosha.Server.ActionHandlers;
using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.Base;
using Etosha.Server.Common.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Etosha.Server.Execution
{
    internal class ActionExecutor : IActionExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IActionExecutor> _logger;
        private static ActionHandlerRegistration _actionHandlers;

        public ActionExecutor(IServiceProvider serviceProvider, ILogger<ActionExecutor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            if (_actionHandlers == null)
            {
                _actionHandlers = new ActionHandlerRegistration();
                var baseType = typeof(AbstractActionHandler);

                foreach (var type in baseType.Assembly.GetTypes().Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t)))
                {
                    _actionHandlers.Register((AbstractActionHandler)ActivatorUtilities.CreateInstance(serviceProvider, type));
                }
            }
        }

        public Task<TResult> Execute<TResult>(AbstractAction<TResult> action) where TResult : AbstractActionResult
        {
            var handler = _actionHandlers.Find(action, _serviceProvider) as AbstractActionHandler<TResult>;

            _logger.LogDebug($"Start executing handler for action {action.Name}.");

            var result = handler.Execute(action);

            _logger.LogDebug($"End executing handler for action {action.Name}.");

            return result;
        }
    }
}
