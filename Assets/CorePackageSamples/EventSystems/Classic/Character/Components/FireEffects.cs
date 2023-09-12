using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class FireEffects : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private ParticleSystem _emitter;

        private IEventManager _eventManager;


        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnWeaponFired>(OnWeaponFired);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnWeaponFired>(OnWeaponFired);
        }

        private void OnWeaponFired(OnWeaponFired eArgs)
        {
            _emitter.Play();
        }
    }
}
