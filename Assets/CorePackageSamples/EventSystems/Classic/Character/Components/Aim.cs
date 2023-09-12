using CorePackage.EventSystems.Classic;
using CorePackageSamples.ClassicEvents.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CorePackageSamples.ClassicEvents.Components
{
    [RequireComponent(typeof(IEventManager))]
    public sealed class Aim : MonoBehaviour, ICharacterComponent
    {
        [SerializeField]
        private Rigidbody2D _turretRotator;

        private IEventManager _eventManager;
        private Vector3 _mouseWorldPosition;
        private Transform _turretTransform;
        private Camera _mainCamera;


        private void Awake()
        {
            _eventManager = GetComponent<IEventManager>();
            _mainCamera = Camera.main;

            _turretTransform = _turretRotator.transform;
            _mouseWorldPosition = _turretTransform.position + Vector3.right;
        }

        private void OnValidate()
        {
            this.AssertObjectField(_turretRotator, "Turret Rotator");
        }

        private void OnEnable()
        {
            _eventManager.Subscribe<OnInputPointerChanged>(OnPointerMoved);
        }

        private void OnDisable()
        {
            _eventManager.Unsubscribe<OnInputPointerChanged>(OnPointerMoved);
        }

        private void FixedUpdate()
        {
            _turretTransform.right = _mouseWorldPosition - _turretTransform.position;

            _eventManager.Invoke(new OnCharacterRotated(Vector2.right, _turretTransform.right, _turretTransform.rotation.eulerAngles.z));
        }

        private void OnPointerMoved(OnInputPointerChanged eArgs)
        {
            _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.value);
            _mouseWorldPosition.z = _turretTransform.position.z;
        }
    }
}
