using System;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Encapsulates a generic event to allow dynamic instancing for different <see cref="IEventArgs"/> implementations.
    /// </summary>
    /// <typeparam name="T">Struct implementations are recommended.</typeparam>
    internal interface IEventContainer<T> : IEventContainer where T : IEventArgs
    {
        void Invoke(T eventArgs);
        void Subscribe(Action<T> listener);
        void Unsubscribe(Action<T> listener);
    }
}
