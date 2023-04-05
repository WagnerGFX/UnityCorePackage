using System;
using System.Collections.Generic;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Manages all listeners from different types
    /// </summary>
    public sealed class EventManager : IEventManager
    {
        private readonly Dictionary<Type, object> eventHolderList = new();

        private bool ContainsEventOfType(Type eventType)
        {
            return eventHolderList.ContainsKey(eventType);
        }

        private void InstantiateListener<T>(Type eventType) where T : IEventArgs
        {
            // Instantiate EventHolder for given type on first use
            if (!eventHolderList.ContainsKey(eventType) || eventHolderList[eventType] == null)
            {
                eventHolderList[eventType] = new EventContainer<T>();
            }
        }


        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            Type eventType = typeof(T);

            InstantiateListener<T>(eventType);

            IEventContainer<T> eventContainer = eventHolderList[eventType] as IEventContainer<T>;
            eventContainer.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                IEventContainer<T> eventContainer = eventHolderList[eventType] as IEventContainer<T>;
                eventContainer.Unsubscribe(listener);
            }
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                IEventContainer<T> eventContainer = eventHolderList[eventType] as IEventContainer<T>;
                eventContainer.UnsubscribeAll();
            }
        }

        public void UnsubscribeAll()
        {
            foreach (KeyValuePair<Type, object> entry in eventHolderList)
            {
                IEventContainer eventContainer = entry.Value as IEventContainer;
                eventContainer.UnsubscribeAll();
            }
            eventHolderList.Clear();
        }
        
        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                IEventContainer<T> eventContainer = eventHolderList[eventType] as IEventContainer<T>;
                eventContainer.Invoke(eventArgs);
            }
        }
    }
}