using System;
using System.Linq;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Encapsulates a generic event to allow dynamic instancing for different IEventArgs implementations.
    /// </summary>
    /// <typeparam name="T">Must inherit from IEventArgs. Can be a class or struct.</typeparam>
    internal class EventHolder<T> : IEventHolderClear where T : IEventArgs
    {
        private event Action<T> EventHandler;

        public void Subscribe(Action<T> listener)
        {
            if (EventHandler == null || !EventHandler.GetInvocationList().Contains(listener))
            {
                EventHandler += listener;
            }
        }

        public void Unsubscribe(Action<T> listener)
        {
            EventHandler -= listener;
        }

        public void Invoke(T eventArgs)
        {
            EventHandler?.Invoke(eventArgs);
        }

        public void UnsubscribeAll()
        {
            EventHandler = null;
        }
    }
}