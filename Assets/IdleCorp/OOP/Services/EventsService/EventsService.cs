using System;
using System.Collections.Generic;

namespace IdleCorp.OOP.Services.EventsService
{
    public class EventsService : IService
    {
        private Dictionary<Type, IEvent> _events;
        
        public void Init()
        {
            _events = new Dictionary<Type, IEvent>();
        }

        public void Dispose()
        {
            
        }

        public T GetEvent<T>() where T : IEvent, new()
        {
            var eventType = typeof(T);

            if (_events.TryGetValue(eventType, out var e)) 
                return (T)e;

            return Register<T>(eventType);
        }
        
        private T Register<T>(Type eventType) where T : IEvent
        {
            var e = (IEvent)Activator.CreateInstance(eventType);
            _events.Add(eventType, e);
            return (T)e;
        }
        
    }
}