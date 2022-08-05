namespace CorePackage.ClassicEventSystem
{
    /// <summary>
    /// Interface to create and categorize all events. Structs are recommended.
    /// </summary>
    public interface IEventArgs
    {
        object Sender { get; }
    }
}
