using System;
using System.Collections.Generic;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Manages all listeners from different types
    /// </summary>
    internal sealed class EventHub : IEventManager
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
                eventHolderList[eventType] = new EventHolder<T>();
            }
        }


        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            Type eventType = typeof(T);

            InstantiateListener<T>(eventType);

            EventHolder<T> oEvent = eventHolderList[eventType] as EventHolder<T>;
            oEvent.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                EventHolder<T> oEvent = eventHolderList[eventType] as EventHolder<T>;
                oEvent.Unsubscribe(listener);
            }
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                EventHolder<T> oEvent = eventHolderList[eventType] as EventHolder<T>;
                oEvent.UnsubscribeAll();
            }
        }

        public void UnsubscribeAll()
        {
            foreach (KeyValuePair<Type, object> entry in eventHolderList)
            {
                IEventHolderClear oHolder = entry.Value as IEventHolderClear;
                oHolder.UnsubscribeAll();
            }
            eventHolderList.Clear();
        }

        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                EventHolder<T> oEvent = eventHolderList[eventType] as EventHolder<T>;
                oEvent.Invoke(eventArgs);
            }
        }
    }
}