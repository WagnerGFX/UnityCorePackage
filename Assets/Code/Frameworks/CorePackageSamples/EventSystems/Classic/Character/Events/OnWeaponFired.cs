using CorePackage.EventSystems.Classic;

namespace Character.Events
{
    public struct OnWeaponFired : IEventArgs
    {
        public float FireRate { get; private set; }

        public OnWeaponFired(float fireRate)
        {
            FireRate = fireRate;
        }
    }
}

