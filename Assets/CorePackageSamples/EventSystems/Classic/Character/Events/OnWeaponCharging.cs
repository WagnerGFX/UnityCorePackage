using CorePackage.EventSystems.Classic;

namespace CorePackageSamples.ClassicEvents.Events
{
    public struct OnWeaponCharging : IEventArgs
    {
        /// <summary>
        /// Projectiles per second.
        /// </summary>
        public readonly float FireRate { get; }

        /// <summary>
        /// Seconds per projectile.
        /// </summary>
        public readonly float ChargeCompleteTime { get; }

        /// <summary>
        /// Hou much has already been charged. In seconds.
        /// </summary>
        public readonly float ChargedTime { get; }

        /// <summary>
        /// Charging Normalized between 0 and 1.
        /// </summary>
        public readonly float ChargedTimeNormalized { get; }

        public readonly bool IsReady { get; }

        public OnWeaponCharging(float fireRate, float normalizedTime)
        {
            FireRate = fireRate;

            ChargeCompleteTime = 1 / fireRate;

            ChargedTimeNormalized = normalizedTime;

            ChargedTime = ChargeCompleteTime * ChargedTimeNormalized;

            IsReady = (ChargedTimeNormalized >= 1f);
        }
    }
}
