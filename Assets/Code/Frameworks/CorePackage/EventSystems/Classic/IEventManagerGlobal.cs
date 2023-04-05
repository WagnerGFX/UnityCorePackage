namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Event Manager that handles events globally and propagates them to subscribed local managers.
    /// </summary>
    public interface IEventManagerGlobal : IEventManager
    {
        public void SubscribeLocalEventManager(IEventManager localEventManager);
        public void UnsubscribeLocalEventManager(IEventManager localEventManager);
    }
}
