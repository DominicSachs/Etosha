using Etosha.Server.ActionHandlers.Base;
using Etosha.Server.Common.Actions.Base;
using System.Collections.Generic;

namespace Etosha.Server.ActionHandlers
{
	internal class ActionHandlerRegistration
	{
		private Dictionary<string, AbstractActionHandler> _items;

		internal ActionHandlerRegistration()
		{
			_items = new Dictionary<string, AbstractActionHandler>();
		}

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
	}
}
