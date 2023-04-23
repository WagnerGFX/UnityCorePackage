using System;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Manages events grouped by implementatios of <see cref="IEventArgs"/>.
    /// </summary>
    public interface IEventManager
    {
        void Invoke<T>(T eventArgs) where T : IEventArgs;
        void Subscribe<T>(Action<T> listener) where T : IEventArgs;
        void Unsubscribe<T>(Action<T> listener) where T : IEventArgs;
        void UnsubscribeAll();
        void UnsubscribeAllOfType<T>() where T : IEventArgs;
    }
}