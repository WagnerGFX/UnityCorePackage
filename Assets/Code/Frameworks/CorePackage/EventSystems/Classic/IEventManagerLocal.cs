using System;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Event Managers that can invoke events globally or locally.
    /// </summary>
    public interface IEventManagerLocal : IEventManager
    {
        void Invoke<T>(T eventArgs, bool sendGoballly) where T : IEventArgs;
        void InvokeGlobal<T>(T eventArgs) where T : IEventArgs;
        void InvokeLocal<T>(T eventArgs) where T : IEventArgs;
    }
}