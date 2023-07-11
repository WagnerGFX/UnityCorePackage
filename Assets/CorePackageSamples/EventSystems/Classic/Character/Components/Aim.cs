using CorePackageSamples.ClassicEvents.Events;
using CorePackage.EventSystems.Classic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Aim : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Rigidbody2D m_turretRotator;

        private IEventManager m_eventManager;
        private Vector3 m_mouseWorldPosition;
        private Transform m_turretTransform;
        private Camera m_mainCamera;


        private void Awake()
        {
            m_eventManager = GetComponent<IEventManager>();
            m_mainCamera = Camera.main;

            m_turretTransform = m_turretRotator.transform;
            m_mouseWorldPosition = m_turretTransform.position + Vector3.right;
        }

        private void OnValidate()
        {
            this.AssertObjectField(m_turretRotator, "Turret Rotator");
        }

        private void OnEnable()
        {
            m_eventManager.Subscribe<OnInputPointerChanged>(OnPointerMoved);
        }

        private void OnDisable()
        {
            m_eventManager.Unsubscribe<OnInputPointerChanged>(OnPointerMoved);
        }

        private void FixedUpdate()
        {
            m_turretTransform.right = m_mouseWorldPosition - m_turretTransform.position;

            m_eventManager.Invoke(new OnCharacterRotated(Vector2.right, m_turretTransform.right, m_turretTransform.rotation.eulerAngles.z));
        }

        private void OnPointerMoved(OnInputPointerChanged eArgs)
        {
            m_mouseWorldPosition = m_mainCamera.ScreenToWorldPoint(Mouse.current.position.value);
            m_mouseWorldPosition.z = m_turretTransform.position.z;
        }
    }
}