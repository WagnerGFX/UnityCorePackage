namespace CorePackage.ClassicEventSystem
{
    /// <summary>
    /// Used to call the ClearListeners() without defining a generic type
    /// </summary>
    internal interface IEventHolderClear
    {
        void UnsubscribeAll();
    }
}
