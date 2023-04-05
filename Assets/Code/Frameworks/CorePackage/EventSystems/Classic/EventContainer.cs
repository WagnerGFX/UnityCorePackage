using System;
using System.Linq;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Default implementation of <see cref="IEventContainer{T}"/>.
    /// </summary>
    /// <typeparam name="T">Struct implementations are recommended.</typeparam>
    internal sealed class EventContainer<T> : IEventContainer<T> where T : IEventArgs
    {
        private event Action<T> EventHandler;

        public void Invoke(T eventArgs)
        {
            EventHandler?.Invoke(eventArgs);
        }

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

        public void UnsubscribeAll()
        {
            EventHandler = null;
        }
    }
}