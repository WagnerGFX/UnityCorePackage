using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Events
{
    public struct OnCharacterMoved : IEventArgs
    {
        public Vector2 CurrentPosition { get; private set; }

        public Vector2 MoveDirection { get; private set; }

        public float MoveSpeed { get; private set; }

        public OnCharacterMoved(Vector2 currentPosition, Vector2 moveDirection, float moveSpeed)
        {
            CurrentPosition = currentPosition;
            MoveDirection = moveDirection;
            MoveSpeed = moveSpeed;
        }
    }
}
