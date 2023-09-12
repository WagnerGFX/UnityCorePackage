using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    public class FireSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        private IEventManager _eventManager;

        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();
        }

        private void OnValidate()
        {
            this.AssertObjectField(_audioSource, "Audio Source");
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnWeaponFired>(PlayWeaponSound);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnWeaponFired>(PlayWeaponSound);
        }

        private void PlayWeaponSound(OnWeaponFired eArgs)
        {
            float nextPitch = Random.Range(0.9f, 1.11f);
            _audioSource.pitch = nextPitch;
            _audioSource.Play();
        }
    }
}
