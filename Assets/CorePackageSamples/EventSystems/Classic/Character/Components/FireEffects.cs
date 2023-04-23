using Character.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace Character.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class FireEffects : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private ParticleSystem m_emitter;

        private IEventManager m_eventManager;


        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnWeaponFired>(OnWeaponFired);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnWeaponFired>(OnWeaponFired);
        }

        private void OnWeaponFired(OnWeaponFired eArgs)
        {
            m_emitter.Play();
        }
    }
}
