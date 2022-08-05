using CorePackage.ClassicEventSystem;

namespace MyProjectName.Events
{
    public struct OnSomethingHappened : IEventArgs
    {
        public object Sender { get; private set; }

        public int Value { get; private set; }

        public OnSomethingHappened(object sender, int value)
        {
            Sender = sender;
            Value = value;
        }
    } 
}