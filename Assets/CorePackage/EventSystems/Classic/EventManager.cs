using System;
using System.Collections.Generic;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Instance-based event manager. Used internally by all other managers.
    /// </summary>
    public sealed class EventManager : IEventManager
    {
        private readonly Dictionary<Type, object> _eventContainerList = new();

        private bool ContainsEventOfType(Type eventType)
        {
            return _eventContainerList.ContainsKey(eventType);
        }

        private void InstantiateListener<T>(Type eventType) where T : IEventArgs
        {
            /// Instantiate a new container for the given type on first use
            if (!_eventContainerList.ContainsKey(eventType) || _eventContainerList[eventType] == null)
            {
                _eventContainerList[eventType] = new EventContainer<T>();
            }
        }


        public void Invoke<T>(T eventArgs) where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                IEventContainer<T> eventContainer = _eventContainerList[eventType] as IEventContainer<T>;
                eventContainer.Invoke(eventArgs);
            }
        }

        public void Subscribe<T>(Action<T> listener) where T : IEventArgs
        {
            Type eventType = typeof(T);

            InstantiateListener<T>(eventType);

            IEventContainer<T> eventContainer = _eventContainerList[eventType] as IEventContainer<T>;
            eventContainer.Subscribe(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                IEventContainer<T> eventContainer = _eventContainerList[eventType] as IEventContainer<T>;
                eventContainer.Unsubscribe(listener);
            }
        }

        public void UnsubscribeAll()
        {
            foreach (KeyValuePair<Type, object> entry in _eventContainerList)
            {
                IEventContainer eventContainer = entry.Value as IEventContainer;
                eventContainer.UnsubscribeAll();
            }
            _eventContainerList.Clear();
        }

        public void UnsubscribeAllOfType<T>() where T : IEventArgs
        {
            Type eventType = typeof(T);

            if (ContainsEventOfType(eventType))
            {
                IEventContainer<T> eventContainer = _eventContainerList[eventType] as IEventContainer<T>;
                eventContainer.UnsubscribeAll();
            }
        }
    }
}
