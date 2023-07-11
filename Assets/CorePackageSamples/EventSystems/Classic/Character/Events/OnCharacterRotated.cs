using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Events
{
    public struct OnCharacterRotated : IEventArgs
    {
        /// <summary>
        /// Initial direction where <see cref="AimAngle"/> is zero.
        /// </summary>
        public Vector2 AimOrigin { get; private set; }


        public Vector2 AimDirection { get; private set; }

        /// <summary>
        /// The Z axis angle relative to the <see cref="AimOrigin"/>.
        /// </summary>
        public float AimAngle { get; private set; }

        
        

        public OnCharacterRotated(Vector2 originDirection, Vector2 aimDirection, float aimAngle)
        {
            AimDirection = aimDirection;
            AimAngle = aimAngle;
            AimOrigin = originDirection;
        }
    }
}
