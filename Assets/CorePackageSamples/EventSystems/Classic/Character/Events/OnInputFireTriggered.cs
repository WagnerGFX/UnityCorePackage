using CorePackage.EventSystems.Classic;

namespace CorePackageSamples.ClassicEvents.Events
{
    public struct OnInputFireTriggered : IEventArgs
    {
        public bool Pressed { get; private set; }

        public OnInputFireTriggered(bool pressed)
        {
            Pressed = pressed;
        }
    }
}
