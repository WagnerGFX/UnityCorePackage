namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Allows to unsubscribe events without defining a generic type
    /// </summary>
    internal interface IEventContainer
    {
        void UnsubscribeAll();
    }
}
