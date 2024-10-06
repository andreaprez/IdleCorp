using System;

namespace IdleCorp.OOP.Services.EventsService
{
    public abstract class Event
    {
        private Action _callback;
        
        public void AddListener(Action listener)
        {
            _callback += listener;
        }
        
        public void RemoveListener(Action listener)
        {
            _callback -= listener;
        }
        
        public void Trigger()
        {
            _callback?.Invoke();
        }
    }
    
    public abstract class Event<T>
    {
        private Action<T> _callback;
        
        public void AddListener(Action<T> listener)
        {
            _callback += listener;
        }
        
        public void RemoveListener(Action<T> listener)
        {
            _callback -= listener;
        }
        
        public void Dispatch(T arg1)
        {
            _callback?.Invoke(arg1);
        }
    }
    
    public abstract class Event<T1, T2>
    {
        private Action<T1, T2> _callback;
        
        public void AddListener(Action<T1, T2> listener)
        {
            _callback += listener;
        }
        
        public void RemoveListener(Action<T1, T2> listener)
        {
            _callback -= listener;
        }
        
        public void Dispatch(T1 arg1, T2 arg2)
        {
            _callback?.Invoke(arg1, arg2);
        }
    }
}