using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Interactions
{
    public interface IDamager
    {
        float Damage { get; }
        float Force { get; }
        float Speed { get; }
        Vector2 Direction { get; }
    }
}
