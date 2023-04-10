using Character.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;

namespace Character.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Mover : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Rigidbody2D m_rigidbody;

        [SerializeField]
        [Tooltip("Speed in units per second.")]
        private float m_movementSpeed = 1f;

        private IEventManager m_eventManager;
        private Vector2 m_previousPosition;


        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();
        }

        private void OnValidate()
        {
            this.AssertObjectField(m_rigidbody, "Rigidbody");
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnInputMoveChanged>(OnMoveChanged);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnInputMoveChanged>(OnMoveChanged);
        }

        private void LateUpdate()
        {
            if (m_rigidbody.position != m_previousPosition)
            {
                m_eventManager.Invoke(new OnCharacterMoved(m_rigidbody.position, m_rigidbody.velocity.normalized, m_rigidbody.velocity.magnitude));
                m_previousPosition = m_rigidbody.position;
            }
        }

        private void OnMoveChanged(OnInputMoveChanged eArgs)
        {
            m_rigidbody.velocity = m_movementSpeed * eArgs.MoveDirection;
        }
    }
}