using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.EventBus
{
    public class DemoEventBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _handlers = new();
        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<Action<object>>();
            }
            _handlers[type].Add(x => handler((T)x));
        }
        public void Publish(object eventData)
        {
            var type = eventData.GetType();
            if (_handlers.ContainsKey(type))
            {
                foreach (var handler in _handlers[type])
                {
                    handler(eventData);
                }
            }
        }
    }
}
