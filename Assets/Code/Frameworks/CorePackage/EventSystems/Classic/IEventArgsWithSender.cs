namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Use when a sender is required.
    /// </summary>
    public interface IEventArgsWithSender : IEventArgs
    {
        object Sender { get; }
    }
}
