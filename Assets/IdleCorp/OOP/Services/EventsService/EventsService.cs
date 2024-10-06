using System;
using System.Collections.Generic;

namespace IdleCorp.OOP.Services.EventsService
{
    public class EventsService : IService
    {
        private Dictionary<Type, Event> _events;
        
        public void Init()
        {
            _events = new Dictionary<Type, Event>();
        }

        public void Dispose()
        {
            
        }

        public Event GetEvent<T>() where T : Event, new()
        {
            var eventType = typeof(T);

            if (_events.TryGetValue(eventType, out var e)) 
                return e;

            return Register(eventType);
        }
        
        private Event Register(Type eventType)
        {
            var e = (Event)Activator.CreateInstance(eventType);
            _events.Add(eventType, e);
            return e;
        }
        
    }
}