using CorePackageSamples.ClassicEvents.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Fire : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Transform m_FirePosition;

        [SerializeField]
        private GameObject m_bullet;

        [SerializeField]
        [Tooltip("Bullets per second")]
        [Range(0.1f, 30f)]
        private float m_fireRate = 10f;

        private float m_timer = 0f;
        private bool m_shoot = false;
        private IEventManager m_eventManager;


        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();

            m_timer = 1 / m_fireRate;
        }

        private void OnValidate()
        {
            this.AssertObjectField(m_FirePosition, "Fire Position");
            this.AssertObjectField(m_bullet, "Bullet Prefab");
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnInputFireTriggered>(OnFireTriggered);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnInputFireTriggered>(OnFireTriggered);
        }

        private void FixedUpdate()
        {
            float timeToNextShot = 1 / m_fireRate;

            if (m_timer < timeToNextShot)
            {
                m_timer += Time.fixedDeltaTime;
            }

            if (m_timer >= timeToNextShot && m_shoot)
            {
                m_timer %= timeToNextShot;

                Instantiate(m_bullet, m_FirePosition.position, m_FirePosition.rotation);
                m_eventManager.Invoke(new OnWeaponFired(timeToNextShot));
            }

            m_eventManager.Invoke(new OnWeaponCharging(m_fireRate, m_timer / timeToNextShot));
        }

        private void OnFireTriggered(OnInputFireTriggered eArgs)
        {
            m_shoot = eArgs.Pressed;
        }
    }
}
