using CorePackageSamples.ClassicEvents.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    public class FireSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        private IEventManager m_eventManager;

        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();
        }

        private void OnValidate()
        {
            this.AssertObjectField(_audioSource, "Audio Source");
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnWeaponFired>(PlayWeaponSound);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnWeaponFired>(PlayWeaponSound);
        }

        private void PlayWeaponSound(OnWeaponFired eArgs)
        {
            float nextPitch = Random.Range(0.9f, 1.11f);
            _audioSource.pitch = nextPitch;
            _audioSource.Play();
        }
    }
}
