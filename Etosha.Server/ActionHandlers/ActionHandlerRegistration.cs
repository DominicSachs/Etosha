using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Etosha.Server.ActionHandlers
{
	internal class ActionHandlerRegistration
	{
		private readonly Dictionary<string, AbstractActionHandler> _items;
		private readonly Dictionary<string, Type> _items2;

		internal ActionHandlerRegistration()
		{
			_items = new Dictionary<string, AbstractActionHandler>();
			_items2 = new Dictionary<string, Type>();
		}

		internal AbstractActionHandler Find(AbstractAction action)
		{
			_items.TryGetValue(action.Name, out var result);
			return result;
		}

		internal AbstractActionHandler Find(AbstractAction action, IServiceProvider serviceProvider)
		{
			_items2.TryGetValue(action.Name, out var result);
			return (AbstractActionHandler)ActivatorUtilities.CreateInstance(serviceProvider, result);
		}

		internal void Register(AbstractActionHandler handler)
		{
			_items.Add(handler.ActionName, handler);
		}

		internal void Register2(AbstractActionHandler handler)
		{
			_items2.Add(handler.ActionName, handler.GetType());
		}

		internal void Unregister(AbstractActionHandler handler)
		{
			_items.Remove(handler.ActionName);
		}
	}
}
