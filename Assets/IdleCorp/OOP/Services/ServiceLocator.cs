using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleCorp.OOP.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, IService> _services = new();

        public static void RegisterService<T>(IService service) where T : IService
        {
            var serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
            {
                Debug.LogError($"ServiceLocator:RegisterService - Service {serviceType.Name} is already registered.");
                return;
            }
            
            _services.Add(serviceType, service);
            service.Init();
        }

        public static T GetService<T>() where T : IService
        {
            var serviceType = typeof(T);

            if (_services.TryGetValue(serviceType, out var service))
                return (T)service;
            
            Debug.LogError($"ServiceLocator:GetService - Service {serviceType.Name} is not registered.");
            return default;
        }
        
    }
}