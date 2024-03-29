using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Events
{
    public struct OnInputMoveChanged : IEventArgs
    {
        public Vector2 MoveDirection { get; private set; }

        public OnInputMoveChanged(Vector2 moveDir)
        {
            MoveDirection = moveDir;
        }
    }
}
