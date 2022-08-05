using System;
using System.Linq;

namespace CorePackage.ClassicEventSystem
{
    /// <summary>
    /// Encapsulates the event handler to allow dynamic instancing for implemented IEventArgs types.
    /// </summary>
    /// <typeparam name="T">Type must inherit from IEventArgs</typeparam>
    internal class EventHolder<T> : IEventHolderClear where T : IEventArgs
    {
        private event Action<T> evHandler;

        public void Subscribe(Action<T> listener)
        {
            if (evHandler == null || !evHandler.GetInvocationList().Contains(listener))
            {
                evHandler += listener;
            }
        }

        public void Unsubscribe(Action<T> listener)
        {
            evHandler -= listener;
        }

        public void Invoke(T eventArgs)
        {
            evHandler?.Invoke(eventArgs);
        }

        public void UnsubscribeAll()
        {
            evHandler = null;
        }
    }
}