using System;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Encapsulates a generic event to allow dynamic instancing for different IEventArgs implementations.
    /// </summary>
    /// <typeparam name="T">Must inherit from IEventArgs. Can be a class or struct.</typeparam>
    internal interface IEventContainer<T> : IEventContainer where T : IEventArgs
    {
        void Invoke(T eventArgs);
        void Subscribe(Action<T> listener);
        void Unsubscribe(Action<T> listener);
    }
}
