using CorePackage.EventSystems.Classic;

namespace Character.Events
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
