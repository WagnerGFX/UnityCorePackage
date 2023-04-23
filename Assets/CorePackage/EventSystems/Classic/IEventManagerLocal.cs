namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Event Manager that handles events in the local scope, but can also send them to the global manager.
    /// </summary>
    public interface IEventManagerLocal : IEventManager
    {
        void Invoke<T>(T eventArgs, bool asGlobal) where T : IEventArgs;
        void InvokeGlobal<T>(T eventArgs) where T : IEventArgs;
        void InvokeLocal<T>(T eventArgs) where T : IEventArgs;
    }
}