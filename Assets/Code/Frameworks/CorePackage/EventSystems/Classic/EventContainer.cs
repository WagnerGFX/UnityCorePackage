using System;
using System.Linq;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Default implementation of IEventContainer<T>.
    /// </summary>
    /// <typeparam name="T">Must inherit from IEventArgs. Can be a class or struct.</typeparam>
    internal sealed class EventContainer<T> : IEventContainer<T> where T : IEventArgs
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