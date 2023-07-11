using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Events
{
    public struct OnInputPointerChanged : IEventArgs
    {
        public Vector2 MoveDirection { get; private set; }

        public OnInputPointerChanged(Vector2 moveDir)
        {
            MoveDirection = moveDir;
        }
    }
}
